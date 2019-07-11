import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { SignupModel } from '../../models/auth';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styles: []
})
export class SignupComponent implements OnInit {
  maskPassword: string = 'password'; 

  signupForm: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
    email: new FormControl('')
  });

  constructor(private router: Router, private authService: AuthService) {
    this.onSignupSuccess = this.onSignupSuccess.bind(this);
    this.onSingupFailed = this.onSingupFailed.bind(this);
  }

  ngOnInit() {
  }

  onSubmit(): void {
    this.authService.signUp(<SignupModel>this.signupForm.value, this.onSignupSuccess, this.onSingupFailed);
  }

  onSignupSuccess(): void {
    this.router.navigate(['/home']); 
  }

  onSingupFailed(response: HttpErrorResponse): void {
    alert(response.error);
  }

  togglePasswordMask(showOrHide: string): void {
    if (showOrHide == 'show') {
      this.maskPassword = 'text';
    }
    else {
      this.maskPassword = 'password';
    }
  }
}
