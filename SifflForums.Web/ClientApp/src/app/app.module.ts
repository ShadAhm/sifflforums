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
import { CommentViewComponent } from './components/comment-view/comment-view.component';
import { SubmissionListItemComponent } from './components/submission-list-item/submission-list-item.component';

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
    CommentViewComponent,
    SubmissionListItemComponent
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
