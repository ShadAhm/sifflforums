import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClientService } from '../util-services/http-client.service';
import { ForumSection } from '../models/forums';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ForumsectionsService extends BaseService {
  constructor(private httpClient: HttpClientService) { super(); }

  getAll(): Observable<ForumSection[]> {
    return this.httpClient.get<ForumSection[]>(`${this.apiRoot}api/forumSections`)
      .pipe(map(res => res));
  }

  getById(forumSectionId: string): Observable<ForumSection> {
    return this.httpClient.get<ForumSection>(`${this.apiRoot}api/forumSections/${forumSectionId}`)
      .pipe(map(res => res));
  }

}
