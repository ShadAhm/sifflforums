import { Injectable } from '@angular/core';
import { SignupModel, TokenModel } from '../models/auth';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {
  constructor(private httpClient: HttpClient) { super(); }

  authCheck(): any {

  }

  signUp(input: SignupModel): Observable<TokenModel> {
    return this.httpClient.post<TokenModel>(`${this.apiRoot}api/auth/signup`, input, this.httpOptions)
      .pipe(map(res => res));
  }
}
