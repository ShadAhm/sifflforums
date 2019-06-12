import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { SubmissionComponent } from './components/submission/submission.component';
import { SubmissionCreateComponent } from './components/submission-create/submission-create.component';

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
    data: { title: 'New Submission' }
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
