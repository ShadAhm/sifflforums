import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommentPost } from '../models/comments';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {
  apiRoot = 'http://localhost:60993/'; 

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient) { }

  getComments(commentThreadId: number): Observable<CommentPost[]> {
    return this.httpClient.get<CommentPost[]>(`${this.apiRoot}api/comments?commentThreadId=${commentThreadId}`)
      .pipe(map(res => res));
  }

  postComment(input: CommentPost): Observable<any> {
    return this.httpClient.post<CommentPost>(`${this.apiRoot}api/comments`, input, this.httpOptions)
      .pipe(map(res => res));
  }
}
