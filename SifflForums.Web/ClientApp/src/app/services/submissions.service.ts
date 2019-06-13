import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { HttpClient } from '@angular/common/http';
import { Submission } from '../models/comments';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class SubmissionsService extends BaseService {
  constructor(private httpClient: HttpClient) { super(); }

  getSubmission(submissionId: number): Observable<Submission> {
    return this.httpClient.get<Submission>(`${this.apiRoot}api/submissions/${submissionId}`)
      .pipe(map(res => res));
  }

  getSubmissions(): Observable<Submission[]> {
    return this.httpClient.get<Submission[]>(`${this.apiRoot}api/submissions`)
      .pipe(map(res => res));
  }

  postThread(input: Submission): Observable<any> {
    return this.httpClient.post<Submission>(`${this.apiRoot}api/submissions`, input, this.httpOptions)
      .pipe(map(res => res));
  }
}
