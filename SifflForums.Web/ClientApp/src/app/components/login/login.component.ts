import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { SignupModel } from '../../models/auth';

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

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  onSubmit(): void {
    this.authService.signUp(<SignupModel>this.loginForm.value).subscribe(
      (response: any) => {
        console.log(response);
      },
      (error) => { console.error("Error happened", error) }
    );
  }
}
