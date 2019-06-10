import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommentsService } from '../../services/comments.service';
import { CommentPost } from '../../models/comments';


@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styles: []
})
export class CommentComponent implements OnInit {
  commentThreadId: number;
  comments: CommentPost[];
  commentInput: string; 

  constructor(private route: ActivatedRoute, private commentsService: CommentsService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.commentThreadId = params['commentThreadId'];
      this.getComments(); 
    });
  }

  getComments(): void {
    this.commentsService.getComments(this.commentThreadId).subscribe(
      (response: CommentPost[]) => { this.comments = response },
      (error) => { console.error("Error happened", error) }
    );
  }

  postComment(): void {
    var input = new CommentPost();
    input.text = this.commentInput;
    input.commentThreadId = this.commentThreadId;

    this.commentsService.postComment(input).subscribe(
      (response) => { console.log('OK', response); },
      (error) => { console.error("Error happened", error) }
    );
  }
}
