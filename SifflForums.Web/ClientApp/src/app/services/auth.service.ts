import { Injectable } from '@angular/core';
import { SignupModel } from '../models/auth';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {
  constructor(private httpClient: HttpClient) { super(); }

  authCheck(): any {

  }

  signUp(input: SignupModel): any {
    return this.httpClient.post<SignupModel>(`${this.apiRoot}api/auth/signup`, input, this.httpOptions)
      .pipe(map(res => res));
  }
}
