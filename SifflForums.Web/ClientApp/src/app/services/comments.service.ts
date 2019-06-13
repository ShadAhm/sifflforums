import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { HttpClient } from '@angular/common/http';
import { CommentPost } from '../models/comments';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CommentsService extends BaseService {
  constructor(private httpClient: HttpClient) { super(); }

  getComments(submissionId: number): Observable<CommentPost[]> {
    return this.httpClient.get<CommentPost[]>(`${this.apiRoot}api/comments?submissionId=${submissionId}`)
      .pipe(map(res => res));
  }

  postComment(input: CommentPost): Observable<any> {
    return this.httpClient.post<CommentPost>(`${this.apiRoot}api/comments`, input, this.httpOptions)
      .pipe(map(res => res));
  }
}
