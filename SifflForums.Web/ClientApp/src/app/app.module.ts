import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 

import { HomeComponent } from './components/home/home.component';
import { CommentComponent } from './components/comment/comment.component';
import { SubmissionCreateComponent } from './components/submission-create/submission-create.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CommentComponent,
    SubmissionCreateComponent
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
