import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { SignupModel } from '../../models/auth';

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

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  onSubmit(): void {
    this.authService.signUp(<SignupModel>this.signupForm.value).subscribe(
      (response: any) => {
        console.log(response);
      },
      (error) => { console.error("Error happened", error) }
    );
  }
}
