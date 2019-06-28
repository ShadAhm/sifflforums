import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { HttpClient, HttpHeaders, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {
  constructor(private authService: AuthService, private http: HttpClient) { }

  createAuthorizationHeader(headers: HttpHeaders): void {
    headers.append('Authorization', this.authService.getAuthorizationHeader());
  }

  get<T>(url): Observable<T> {
    let headers = new HttpHeaders();
    this.createAuthorizationHeader(headers);
    return this.http.get<T>(url, { headers: headers });
  }

  post<T>(url, data, options) {
    this.createAuthorizationHeader(options.headers);
    return this.http.post<T>(url, data, options);
  }

  put<T>(url, data, options) {
    this.createAuthorizationHeader(options.headers);
    return this.http.put<T>(url, data, options);
  }

  delete<T>(url, options) {
    this.createAuthorizationHeader(options.headers);
    return this.http.delete<T>(url, options);
  }
}


