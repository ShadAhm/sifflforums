import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Submission } from '../models/comments';

@Injectable({
  providedIn: 'root'
})
export class SubmissionsService {
  private apiRoot = 'http://localhost:60993/'; 
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient) { }

  getThreads(): Observable<Submission[]> {
    return this.httpClient.get<Submission[]>(`${this.apiRoot}api/submissions`)
      .pipe(map(res => res));
  }

  postThread(input: Submission): Observable<any> {
    return this.httpClient.post<Submission>(`${this.apiRoot}api/submissions`, input, this.httpOptions)
      .pipe(map(res => res));
  }
}
