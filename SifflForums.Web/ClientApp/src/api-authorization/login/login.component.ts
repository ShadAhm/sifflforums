import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../authorize.service';
import { ApplicationPaths, LoginActions, QueryParameterNames } from '../api-authorization.constants';

// Handles both login and registration against the API's ASP.NET Core Identity endpoints.
// Any component that needs to authenticate a user can redirect here with a returnUrl
// query parameter; after a successful login the user is sent back to that url.
@Component({
  selector: 'app-login',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.Eager,
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  isRegister: boolean;
  isSubmitting: boolean;
  errorMessage: string;
  applicationPaths = ApplicationPaths;

  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('')
  });

  constructor(
    private authorizeService: AuthorizeService,
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.activatedRoute.url.subscribe(url => {
      this.isRegister = url[1]?.path === LoginActions.Register;
      this.errorMessage = null;
      // Registration needs an email address, but login also accepts a plain username (e.g. "admin")
      const email = this.form.controls.email;
      email.setValidators(this.isRegister ? [Validators.required, Validators.email] : Validators.required);
      email.updateValueAndValidity();
    });
  }

  get returnUrl(): string {
    return this.activatedRoute.snapshot.queryParamMap.get(QueryParameterNames.ReturnUrl);
  }

  onSubmit(): void {
    if (!this.form.valid) {
      this.errorMessage = this.isRegister
        ? 'Please enter a valid email and password.'
        : 'Please enter your email or username and password.';
      return;
    }

    const { email, password, confirmPassword } = this.form.value;
    if (this.isRegister && password !== confirmPassword) {
      this.errorMessage = 'Passwords do not match.';
      return;
    }

    const request: Observable<void> = this.isRegister
      ? this.authorizeService.register(email, password)
      : this.authorizeService.login(email, password);

    this.isSubmitting = true;
    this.errorMessage = null;
    request.subscribe({
      next: () => this.navigateToReturnUrl(),
      error: (error: Error) => {
        this.errorMessage = error.message;
        this.isSubmitting = false;
      }
    });
  }

  private navigateToReturnUrl(): void {
    const returnUrl = this.returnUrl;
    // Prevent open redirects: only allow local, app-relative urls
    const target = returnUrl && returnUrl.startsWith('/') && !returnUrl.startsWith('//')
      ? returnUrl
      : ApplicationPaths.DefaultLoginRedirectPath;

    this.router.navigateByUrl(target, { replaceUrl: true });
  }
}
