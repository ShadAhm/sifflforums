import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CommentComponent } from './components/comment/comment.component';
import { SubmissionCreateComponent } from './components/submission-create/submission-create.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    component: HomeComponent,
    data: { title: 'Home' }
  },
  {
    path: 'comments/:submissionId',
    component: CommentComponent,
    data: { title: 'Comments' }
  },
  {
    path: 'submission/new',
    component: SubmissionCreateComponent,
    data: { title: 'New Submission' }
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
