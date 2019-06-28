import { HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { CommentPost } from '../models/comments';
import { HttpClientService } from '../util-services/http-client.service';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CommentsService extends BaseService {
  constructor(private httpClient: HttpClientService) { super(); }

  getComments(submissionId: number): Observable<CommentPost[]> {
    return this.httpClient.get<CommentPost[]>(`${this.apiRoot}api/comments?submissionId=${submissionId}`)
      .pipe(map(res => res));
  }

  postComment(input: CommentPost): Observable<CommentPost> {
    return this.httpClient.post<CommentPost>(`${this.apiRoot}api/comments`, input, this.httpOptions)
      .pipe(map(res => res));
  }
}
