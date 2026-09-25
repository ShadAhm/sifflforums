import { Injectable } from '@angular/core';
import { HttpClient, HttpContext, HttpContextToken, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { catchError, finalize, map, shareReplay, switchMap, tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { AuthApiPaths } from './api-authorization.constants';

export interface IUser {
  name?: string;
  roles?: string[];
}

// Response of the API's GET api/users/me endpoint
interface CurrentUserResponse {
  userName: string;
  roles: string[];
}

export const AdminRole = 'Admin';

// Response of the ASP.NET Core Identity /login and /refresh endpoints
interface AccessTokenResponse {
  tokenType: string;
  accessToken: string;
  expiresIn: number;
  refreshToken: string;
}

interface StoredSession {
  name: string;
  roles: string[];
  accessToken: string;
  refreshToken: string;
  expiresAtUtc: number;
}

// Marks token requests so AuthorizeInterceptor passes them through instead of
// trying to attach (and possibly refresh) a token, which would recurse.
export const SKIP_AUTHORIZATION = new HttpContextToken<boolean>(() => false);

const SessionStorageKey = 'siffl.auth';
// Refresh the access token slightly before it actually expires
const ExpirySkewMs = 30 * 1000;

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {
  private apiRoot = environment.apiRootUrl;
  private userSubject: BehaviorSubject<IUser | null>;
  private refreshInFlight: Observable<string | null> | null = null;

  // Uses the app's HttpClient (not a raw HttpBackend) so responses trigger change detection
  constructor(private http: HttpClient) {
    const session = this.readSession();
    this.userSubject = new BehaviorSubject<IUser | null>(session ? this.toUser(session) : null);
  }

  public isAuthenticated(): Observable<boolean> {
    return this.getUser().pipe(map(u => !!u));
  }

  public getUser(): Observable<IUser | null> {
    return this.userSubject.asObservable();
  }

  public isAdmin(): Observable<boolean> {
    return this.getUser().pipe(map(u => !!u?.roles?.includes(AdminRole)));
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
        catchError(error => throwError(() => new Error(this.getErrorMessage(error, 'Invalid email, username or password.')))),
        switchMap(response => this.getCurrentUser(response.accessToken).pipe(
          tap(user => this.storeSession(user?.userName || email, user?.roles || [], response)))),
        map(() => undefined));
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
          map(response => this.storeSession(session.name, session.roles || [], response).accessToken),
          catchError(() => {
            this.clearSession();
            return of(null);
          }),
          finalize(() => this.refreshInFlight = null),
          shareReplay(1));
    }

    return this.refreshInFlight;
  }

  // Roles aren't readable from the opaque bearer token, so ask the API. A failure here
  // shouldn't block login; the user is just treated as having no roles.
  private getCurrentUser(accessToken: string): Observable<CurrentUserResponse | null> {
    const options = this.jsonOptions();
    return this.http.get<CurrentUserResponse>(`${this.apiRoot}${AuthApiPaths.CurrentUser}`, {
      ...options,
      headers: options.headers.set('Authorization', `Bearer ${accessToken}`)
    }).pipe(catchError(() => of(null)));
  }

  private toUser(session: StoredSession): IUser {
    return { name: session.name, roles: session.roles || [] };
  }

  private storeSession(name: string, roles: string[], response: AccessTokenResponse): StoredSession {
    const session: StoredSession = {
      name,
      roles,
      accessToken: response.accessToken,
      refreshToken: response.refreshToken,
      expiresAtUtc: Date.now() + response.expiresIn * 1000
    };

    localStorage.setItem(SessionStorageKey, JSON.stringify(session));
    this.userSubject.next(this.toUser(session));
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
    return {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      context: new HttpContext().set(SKIP_AUTHORIZATION, true)
    };
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

    // A failed /login returns 401 with a generic detail of "Failed", which isn't useful to show
    if (error?.status === 401) {
      return fallback;
    }

    return error?.error?.detail || fallback;
  }
}
