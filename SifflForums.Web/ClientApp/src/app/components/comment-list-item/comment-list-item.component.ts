import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CommentPost } from '../../models/comments';
import { CommentsService } from '../../services/comments.service';

@Component({
  selector: 'app-comment-list-item',
  templateUrl: './comment-list-item.component.html',
  styleUrls: ['./comment-list-item.component.scss']
})
export class CommentListItemComponent implements OnInit {
  @Input() model: CommentPost;
  @Output() quoted = new EventEmitter<void>();

  constructor(private commentsService: CommentsService) { }

  ngOnInit() {
  }

  quote(): void {
    this.quoted.emit(); 
  }

  upvote(): void {
    this.commentsService.upvote(this.model.commentId).subscribe(
      (response) => { },
      (error) => { }
    );
  }

  downvote(): void {
    this.commentsService.downvote(this.model.commentId).subscribe(
      (response) => { },
      (error) => { }
    );
  }
}
