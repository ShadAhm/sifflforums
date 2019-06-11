import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CommentComponent } from './components/comment/comment.component';
import { CommentThreadCreateComponent } from './components/comment-thread-create/comment-thread-create.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    component: HomeComponent,
    data: { title: 'Home' }
  },
  {
    path: 'comments/:commentThreadId',
    component: CommentComponent,
    data: { title: 'Comments' }
  },
  {
    path: 'thread/new',
    component: CommentThreadCreateComponent,
    data: { title: 'New Thread' }
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
