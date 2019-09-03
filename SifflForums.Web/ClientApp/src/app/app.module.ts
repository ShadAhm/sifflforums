import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { SubmissionCreateComponent } from './components/submission-create/submission-create.component';
import { SubmissionComponent } from './components/submission/submission.component';
import { DateAgoPipe } from './pipes/date-ago.pipe';
import { CommentListItemComponent } from './components/comment-list-item/comment-list-item.component';
import { SubmissionListItemComponent } from './components/submission-list-item/submission-list-item.component';
import { ForumsectionListItemComponent } from './components/forumsection-list-item/forumsection-list-item.component';
import { SubmissionsComponent } from './components/submissions/submissions.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SubmissionComponent,
    SubmissionCreateComponent,
    HeaderComponent,
    SignupComponent,
    LoginComponent,
    DateAgoPipe,
    CommentListItemComponent,
    SubmissionListItemComponent,
    ForumsectionListItemComponent,
    SubmissionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
