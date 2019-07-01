import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  protected apiRoot = 'http://localhost:60993/';

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
