import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { HttpClient, HttpHeaders, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {
  constructor(private http: HttpClient) { }

  createAuthorizationHeader(headers: HttpHeaders): void {
    let token = localStorage.getItem('id_token');

    if (token) {
      headers.append('Authorization', `Bearer ${token}`);
    }
  }

  get<T>(url: string): Observable<T> {
    let headers = new HttpHeaders();
    this.createAuthorizationHeader(headers);
    return this.http.get<T>(url, { headers: headers });
  }

  post<T>(url: string, data: any, options: any): Observable<T> {
    this.createAuthorizationHeader(options.headers);
    return this.http.post<T>(url, data, <Object>options);
  }

  put<T>(url: string, data: any, options: any): Observable<T> {
    this.createAuthorizationHeader(options.headers);
    return this.http.put<T>(url, data, <Object>options);
  }

  delete<T>(url: string, options: any): Observable<T> {
    this.createAuthorizationHeader(options.headers);
    return this.http.delete<T>(url, <Object>options);
  }
}


