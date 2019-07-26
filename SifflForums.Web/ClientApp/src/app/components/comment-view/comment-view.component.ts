import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CommentPost } from '../../models/comments';
import { CommentsService } from '../../services/comments.service';

@Component({
  selector: 'app-comment-view',
  templateUrl: './comment-view.component.html',
  styleUrls: ['./comment-view.component.scss']
})
export class CommentViewComponent implements OnInit {
  @Input() model: CommentPost;
  @Output() quoted = new EventEmitter<void>();

  constructor(private commentsService: CommentsService) { }

  ngOnInit() {
  }

  quote(): void {
    this.quoted.emit(); 
  }

  upvote(): void {
    this.commentsService.upvote(this.model.commentId, this.model.votingBoxId).subscribe(
      (response) => { },
      (error) => { }
    );
  }
}
