import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  protected apiRoot = environment.apiRootUrl;

  protected httpHeaders: HttpHeaders;

  constructor() {
    this.initHttpHeaders();
  }

  initHttpHeaders() {
    this.httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json'
    });
  }
}
