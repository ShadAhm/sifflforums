import { HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SigninModel, SignupModel, TokenModel } from '../models/auth';
import { HttpClientService } from '../util-services/http-client.service';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {
  constructor(private httpClient: HttpClientService) { super(); }

  signUp(input: SignupModel): Observable<HttpEvent<TokenModel>> {
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

  getAuthorizationHeader(): string {
    let token = localStorage.getItem('id_token');

    return `Bearer ${token}`;
  }
}
