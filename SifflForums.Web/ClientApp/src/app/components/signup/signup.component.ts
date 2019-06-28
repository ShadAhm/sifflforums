import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { SignupModel } from '../../models/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styles: []
})
export class SignupComponent implements OnInit {
  signupForm: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
    email: new FormControl('')
  });

  constructor(private router: Router, private authService: AuthService) {
  }

  ngOnInit() {
  }

  onSubmit(): void {
    this.authService.signUp(<SignupModel>this.signupForm.value).subscribe(
      (response: any) => {
        this.router.navigate(['/home']); 
      },
      (error) => { console.error("Error happened", error) }
    );
  }
}
