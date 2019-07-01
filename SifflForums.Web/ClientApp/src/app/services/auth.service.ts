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

  signUp(input: SignupModel, successCallback: Function, errorCallback: Function): void {
    this.httpClient.post<TokenModel>(`${this.apiRoot}api/auth/signup`, input, this.httpHeaders)
      .pipe(map(res => res))
      .subscribe((response: any) => {
        localStorage.setItem('id_token', response.token);
        successCallback();
      },
        (error) => { errorCallback(error) }); 
  }

  signIn(input: SigninModel, successCallback: Function, errorCallback: Function): void {
    this.httpClient.post<TokenModel>(`${this.apiRoot}api/auth/login`, input, this.httpHeaders).pipe(map(res => res))
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
