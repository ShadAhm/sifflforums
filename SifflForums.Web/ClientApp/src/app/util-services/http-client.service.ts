import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { HttpClient, HttpHeaders, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {
  constructor(private http: HttpClient) { }

  createAuthorizationHeader(headers: HttpHeaders): HttpHeaders {
    let token = localStorage.getItem('id_token');

    if (token) {
      return headers.append('Authorization', `Bearer ${token}`);
    }
    return headers; 
  }

  createHttpOptions(headers: HttpHeaders): object {
    return {
      headers: headers
    }
  }

  get<T>(url: string): Observable<T> {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    headers = this.createAuthorizationHeader(headers);
    return this.http.get<T>(url, { headers: headers });
  }

  post<T>(url: string, data: any, headers: HttpHeaders): Observable<T> {
    headers = this.createAuthorizationHeader(headers);
    return this.http.post<T>(url, data, this.createHttpOptions(headers));
  }

  put<T>(url: string, data: any, headers: HttpHeaders): Observable<T> {
    headers = this.createAuthorizationHeader(headers);
    return this.http.put<T>(url, data, this.createHttpOptions(headers));
  }

  delete<T>(url: string, headers: HttpHeaders): Observable<T> {
    headers = this.createAuthorizationHeader(headers);
    return this.http.delete<T>(url, this.createHttpOptions(headers));
  }
}


