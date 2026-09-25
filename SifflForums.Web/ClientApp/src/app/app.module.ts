import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule, provideZoneChangeDetection } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { SubmissionCreateComponent } from './components/submission-create/submission-create.component';
import { SubmissionComponent } from './components/submission/submission.component';
import { DateAgoPipe } from './pipes/date-ago.pipe';
import { CommentListItemComponent } from './components/comment-list-item/comment-list-item.component';
import { SubmissionListItemComponent } from './components/submission-list-item/submission-list-item.component';
import { ForumsectionListItemComponent } from './components/forumsection-list-item/forumsection-list-item.component';
import { SubmissionsComponent } from './components/submissions/submissions.component';
import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from '../api-authorization/authorize.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SubmissionComponent,
    SubmissionCreateComponent,
    HeaderComponent,
    DateAgoPipe,
    CommentListItemComponent,
    SubmissionListItemComponent,
    ForumsectionListItemComponent,
    SubmissionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    ApiAuthorizationModule
  ],
  providers: [
    provideZoneChangeDetection(),
    provideHttpClient(withInterceptorsFromDi()),
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
