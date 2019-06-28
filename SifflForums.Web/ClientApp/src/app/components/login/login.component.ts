import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { SigninModel } from '../../models/auth';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl('')
  });

  constructor(private router: Router, private authService: AuthService) {
    this.onLoginSuccessful = this.onLoginSuccessful.bind(this);
    this.onLoginFailed = this.onLoginFailed.bind(this);
  }

  ngOnInit() {
  }

  onSubmit(): void {
    this.authService.signIn(<SigninModel>this.loginForm.value, this.onLoginSuccessful, this.onLoginFailed);
  }

  onLoginSuccessful(): void {
    this.router.navigate(['/home']); 
  }

  onLoginFailed(error: HttpErrorResponse) {
    alert('Password invalid'); 
  }
}
