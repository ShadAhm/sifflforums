import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { SubmissionComponent } from './components/submission/submission.component';
import { SubmissionCreateComponent } from './components/submission-create/submission-create.component';
import { SignupComponent } from './components/signup/signup.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuardService } from './util-services/auth-guard.service';
import { UnauthGuardService } from './util-services/unauth-guard.service';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    component: HomeComponent,
    data: { title: 'Home' }
  },
  {
    path: 'submission/:submissionId',
    component: SubmissionComponent,
    data: { title: 'Comments' }
  },
  {
    path: 'new-submission',
    component: SubmissionCreateComponent,
    canActivate: [AuthGuardService], 
    data: { title: 'New Submission' }
  },
  {
    path: 'signup',
    component: SignupComponent,
    canActivate: [UnauthGuardService]
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [UnauthGuardService]
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
