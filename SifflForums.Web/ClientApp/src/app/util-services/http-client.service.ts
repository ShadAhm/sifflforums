import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

// The bearer token is attached by AuthorizeInterceptor
@Injectable({
  providedIn: 'root'
})
export class HttpClientService {
  constructor(private http: HttpClient) { }

  createHttpOptions(headers: HttpHeaders): object {
    return {
      headers: headers
    }
  }

  get<T>(url: string): Observable<T> {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.get<T>(url, { headers: headers });
  }

  post<T>(url: string, data: any, headers: HttpHeaders): Observable<T> {
    return this.http.post<T>(url, data, this.createHttpOptions(headers));
  }

  put<T>(url: string, data: any, headers: HttpHeaders): Observable<T> {
    return this.http.put<T>(url, data, this.createHttpOptions(headers));
  }

  delete<T>(url: string, headers: HttpHeaders): Observable<T> {
    return this.http.delete<T>(url, this.createHttpOptions(headers));
  }
}


