import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommentThread } from '../models/comments';

@Injectable({
  providedIn: 'root'
})
export class CommentThreadService {
  private apiRoot = 'http://localhost:60993/'; 
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient) { }

  getThreads(): Observable<CommentThread[]> {
    return this.httpClient.get<CommentThread[]>(`${this.apiRoot}api/commentThreads`)
      .pipe(map(res => res));
  }

  postThread(input: CommentThread): Observable<any> {
    return this.httpClient.post<CommentThread>(`${this.apiRoot}api/commentThreads`, input, this.httpOptions)
      .pipe(map(res => res));
  }
}
