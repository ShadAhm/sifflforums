import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { Submission } from '../models/comments';
import { HttpClientService } from '../util-services/http-client.service';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class SubmissionsService extends BaseService {
  constructor(private httpClient: HttpClientService) { super(); }

  getSubmission(submissionId: number): Observable<Submission> {
    return this.httpClient.get<Submission>(`${this.apiRoot}api/submissions/${submissionId}`)
      .pipe(map(res => res));
  }

  getSubmissions(): Observable<Submission[]> {
    return this.httpClient.get<Submission[]>(`${this.apiRoot}api/submissions`)
      .pipe(map(res => res));
  }

  postThread(input: Submission): Observable<Submission> {
    return this.httpClient.post<Submission>(`${this.apiRoot}api/submissions`, input, this.httpHeaders)
      .pipe(map(res => res));
  }

  upvote(submissionId: number): Observable<void> {
    return this.httpClient.put<void>(`${this.apiRoot}api/submissions/${submissionId}/upvote`, null, this.httpHeaders)
      .pipe(map(res => res));
  }

  downvote(submissionId: number): Observable<void> {
    return this.httpClient.put<void>(`${this.apiRoot}api/submissions/${submissionId}/downvote`, null, this.httpHeaders)
      .pipe(map(res => res));
  }
}
