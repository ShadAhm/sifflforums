import { Component, OnInit, Input } from '@angular/core';
import { CommentPost } from '../../models/comments';
import { CommentsService } from '../../services/comments.service';

@Component({
  selector: 'app-comment-view',
  templateUrl: './comment-view.component.html',
  styleUrls: ['./comment-view.component.scss']
})
export class CommentViewComponent implements OnInit {
  @Input() model: CommentPost;

  constructor(private commentsService: CommentsService) { }

  ngOnInit() {
  }

  quote(): void {

  }

  upvote(): void {
    this.commentsService.upvote(this.model.commentId, this.model.votingBoxId).subscribe(
      (response) => { },
      (error) => { }
    );
  }
}
