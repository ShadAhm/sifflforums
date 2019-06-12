import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { CommentThreadService } from '../../services/comment-thread.service';
import { CommentThread } from '../../models/comments';
import { Router } from '@angular/router';

@Component({
  selector: 'app-comment-thread-create',
  templateUrl: './comment-thread-create.component.html',
  styles: []
})
export class CommentThreadCreateComponent implements OnInit {
  commentThreadForm : FormGroup = new FormGroup({
    title: new FormControl(''),
    text: new FormControl(''),
  });

  constructor(private router: Router, private commentThreadService: CommentThreadService) { }

  ngOnInit() {
  }

  onSubmit(): void {
    var input = new CommentThread();
    input.text = this.commentThreadForm.value.text;
    input.title = this.commentThreadForm.value.title;

    this.commentThreadService.postThread(input).subscribe(
      (response: CommentThread) => {
        this.router.navigateByUrl(`/comments/${response.commentThreadId}`);
      },
      (error) => { console.error("Error happened", error) }
    );
  }
}
