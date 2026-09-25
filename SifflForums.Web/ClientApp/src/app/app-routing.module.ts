import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { SubmissionComponent } from './components/submission/submission.component';
import { SubmissionCreateComponent } from './components/submission-create/submission-create.component';
import { SubmissionsComponent } from './components/submissions/submissions.component';
import { ForumsectionCreateComponent } from './components/forumsection-create/forumsection-create.component';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';

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
  },
  {
    path: 'new-forum-section',
    component: ForumsectionCreateComponent,
    canActivate: [AuthorizeGuard],
    data: { title: 'New Forum Section' }
  },
  {
    path: 'submissions/:forumSectionId',
    component: SubmissionsComponent,
    data: { title: 'Submissions' }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
