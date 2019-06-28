import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  protected apiRoot = 'http://localhost:60993/';

  protected httpOptions: HttpHeaders;

  constructor() {
    this.initHttpHeaders();
  }

  initHttpHeaders() {
    this.httpOptions = new HttpHeaders({
      'Content-Type': 'application/json'
    });
  }
}
