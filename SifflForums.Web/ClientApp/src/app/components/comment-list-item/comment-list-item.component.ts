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
  votePosition: number;
  upvotesCountOnScreen: number;

  constructor(private commentsService: CommentsService) { }

  ngOnInit() {
    this.votePosition = this.model.currentUserVoteWeight;
    this.upvotesCountOnScreen = this.model.upvotes; 
  }

  quote(): void {
    this.quoted.emit(); 
  }

  upvote(): void {
    if (this.votePosition == 1) {
      this.removeVote();
      return;
    }

    this.votePosition = 1;
    this.upvotesCountOnScreen = (this.model.upvotes - this.model.currentUserVoteWeight) + this.votePosition;

    this.commentsService.upvote(this.model.commentId).subscribe(
      (response) => { },
      (error) => { }
    );
  }

  downvote(): void {
    if (this.votePosition == -1) {
      this.removeVote();
      return;
    }

    this.votePosition = -1;
    this.upvotesCountOnScreen = (this.model.upvotes - this.model.currentUserVoteWeight) + this.votePosition;

    this.commentsService.downvote(this.model.commentId).subscribe(
      (response) => { },
      (error) => { }
    );
  }

  removeVote(): void {
    this.votePosition = 0;
    this.upvotesCountOnScreen = (this.model.upvotes - this.model.currentUserVoteWeight) + this.votePosition;

    this.commentsService.removevote(this.model.commentId).subscribe(
      (response) => { },
      (error) => { }
    );
  }
}
