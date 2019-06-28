import { Injectable } from '@angular/core';
import { SignupModel, SigninModel, TokenModel } from '../models/auth';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {
  constructor(private httpClient: HttpClient) { super(); }

  signUp(input: SignupModel): Observable<TokenModel> {
    return this.httpClient.post<TokenModel>(`${this.apiRoot}api/auth/signup`, input, this.httpOptions)
      .pipe(map(res => res));
  }

  signIn(input: SigninModel, successCallback: Function, errorCallback: Function): void { //Observable<TokenModel> {
    this.httpClient.post<TokenModel>(`${this.apiRoot}api/auth/login`, input, this.httpOptions).pipe(map(res => res))
      .subscribe((response: any) => {
        localStorage.setItem('id_token', response.token);
        successCallback();
      },
        (error) => { errorCallback(error) }); 
  }

  signOut(): void {
    localStorage.removeItem('id_token');
  }

  isSignedIn(): boolean {
    let token = localStorage.getItem('id_token');

    if (token) {
      return true;
    }

    return false;
  }
}
