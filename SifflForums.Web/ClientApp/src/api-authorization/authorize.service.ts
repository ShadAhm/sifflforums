import { Injectable } from '@angular/core';
import { HttpBackend, HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { catchError, finalize, map, shareReplay, switchMap, tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { AuthApiPaths } from './api-authorization.constants';

export interface IUser {
  name?: string;
}

// Response of the ASP.NET Core Identity /login and /refresh endpoints
interface AccessTokenResponse {
  tokenType: string;
  accessToken: string;
  expiresIn: number;
  refreshToken: string;
}

interface StoredSession {
  name: string;
  accessToken: string;
  refreshToken: string;
  expiresAtUtc: number;
}

const SessionStorageKey = 'siffl.auth';
// Refresh the access token slightly before it actually expires
const ExpirySkewMs = 30 * 1000;

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {
  // Bypasses HTTP interceptors so token calls don't recurse through AuthorizeInterceptor
  private http: HttpClient;
  private apiRoot = environment.apiRootUrl;
  private userSubject: BehaviorSubject<IUser | null>;
  private refreshInFlight: Observable<string | null> | null = null;

  constructor(httpBackend: HttpBackend) {
    this.http = new HttpClient(httpBackend);
    const session = this.readSession();
    this.userSubject = new BehaviorSubject<IUser | null>(session ? { name: session.name } : null);
  }

  public isAuthenticated(): Observable<boolean> {
    return this.getUser().pipe(map(u => !!u));
  }

  public getUser(): Observable<IUser | null> {
    return this.userSubject.asObservable();
  }

  public getAccessToken(): Observable<string | null> {
    const session = this.readSession();
    if (!session) {
      return of(null);
    }

    if (Date.now() < session.expiresAtUtc - ExpirySkewMs) {
      return of(session.accessToken);
    }

    return this.refresh(session);
  }

  public login(email: string, password: string): Observable<void> {
    return this.http.post<AccessTokenResponse>(`${this.apiRoot}${AuthApiPaths.Login}`, { email, password }, this.jsonOptions())
      .pipe(
        tap(response => this.storeSession(email, response)),
        map(() => undefined),
        catchError(error => throwError(() => new Error(this.getErrorMessage(error, 'Invalid email or password.')))));
  }

  public register(email: string, password: string): Observable<void> {
    return this.http.post(`${this.apiRoot}${AuthApiPaths.Register}`, { email, password }, this.jsonOptions())
      .pipe(
        catchError(error => throwError(() => new Error(this.getErrorMessage(error, 'Registration failed.')))),
        switchMap(() => this.login(email, password)));
  }

  public logout(): void {
    this.clearSession();
  }

  private refresh(session: StoredSession): Observable<string | null> {
    if (!this.refreshInFlight) {
      this.refreshInFlight = this.http.post<AccessTokenResponse>(`${this.apiRoot}${AuthApiPaths.Refresh}`, { refreshToken: session.refreshToken }, this.jsonOptions())
        .pipe(
          map(response => this.storeSession(session.name, response).accessToken),
          catchError(() => {
            this.clearSession();
            return of(null);
          }),
          finalize(() => this.refreshInFlight = null),
          shareReplay(1));
    }

    return this.refreshInFlight;
  }

  private storeSession(name: string, response: AccessTokenResponse): StoredSession {
    const session: StoredSession = {
      name,
      accessToken: response.accessToken,
      refreshToken: response.refreshToken,
      expiresAtUtc: Date.now() + response.expiresIn * 1000
    };

    localStorage.setItem(SessionStorageKey, JSON.stringify(session));
    this.userSubject.next({ name });
    return session;
  }

  private readSession(): StoredSession | null {
    try {
      const raw = localStorage.getItem(SessionStorageKey);
      return raw ? JSON.parse(raw) as StoredSession : null;
    } catch {
      return null;
    }
  }

  private clearSession(): void {
    localStorage.removeItem(SessionStorageKey);
    this.userSubject.next(null);
  }

  private jsonOptions() {
    return { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  }

  // Identity endpoints return ValidationProblemDetails ({ errors: { code: [messages] } }) on failure
  private getErrorMessage(error: HttpErrorResponse, fallback: string): string {
    const errors = error?.error?.errors;
    if (errors) {
      const messages = Object.keys(errors).reduce((all, key) => all.concat(errors[key]), [] as string[]);
      if (messages.length > 0) {
        return messages.join(' ');
      }
    }

    if (error?.status === 0) {
      return 'Unable to reach the server.';
    }

    return error?.error?.detail || fallback;
  }
}
