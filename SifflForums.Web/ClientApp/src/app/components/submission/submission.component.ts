import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SubmissionsService } from '../../services/submissions.service';
import { Submission, CommentPost } from '../../models/comments';
import { CommentsService } from '../../services/comments.service';


@Component({
  selector: 'app-submission',
  templateUrl: './submission.component.html',
  styleUrls: ['./submission.component.scss']
})
export class SubmissionComponent implements OnInit {
  submissionId: string;
  submission: Submission;
  comments: CommentPost[];
  commentInput: string;
  votePosition: number;
  upvotesCountOnScreen: number; 
  disableSubmitButton: boolean;

  constructor(private route: ActivatedRoute, private commentsService: CommentsService, private submissionsService: SubmissionsService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.submissionId = params['submissionId'];
      this.getSubmission();
    });
  }

  getSubmission(): void {
    this.submissionsService.getSubmission(this.submissionId).subscribe(
      (response: Submission) => {
        this.submission = response;
        this.votePosition = this.submission.currentUserVoteWeight;
        this.upvotesCountOnScreen = this.submission.upvotes; 

        if (this.submission.id != null && this.submission.id != '')
          this.getComments(this.submission.id);
      },
      (error) => { console.error("Error happened", error) }
    );
  }

  getComments(parentId: string): void {
    this.commentsService.getComments(parentId).subscribe(
      (response: CommentPost[]) => { this.comments = response },
      (error) => { console.error("Error happened", error) }
    );
  }

  postComment(): void {
    this.disableSubmitButton = true; 

    var input = new CommentPost();
    input.text = this.commentInput;
    input.submissionId = this.submissionId;

    this.commentsService.postComment(input).subscribe(
      (response) => {
        this.comments.push(response);
        this.commentInput = '';
        this.disableSubmitButton = false;
      },
      (error) => { console.error("Error happened", error); this.disableSubmitButton = false; }
    );
  }

  upvote(): void {
    if (this.votePosition == 1) {
      this.removeVote();
      return; 
    }

    this.votePosition = 1;
    this.upvotesCountOnScreen = this.submission.upvotes - this.submission.currentUserVoteWeight + this.votePosition; 

    this.submissionsService.upvote(this.submission.id).subscribe(
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
    this.upvotesCountOnScreen = this.submission.upvotes - this.submission.currentUserVoteWeight + this.votePosition; 

    this.submissionsService.downvote(this.submission.id).subscribe(
      (response) => { },
      (error) => { }
    );
  }

  removeVote(): void {
    this.votePosition = 0;
    this.upvotesCountOnScreen = this.submission.upvotes;

    this.submissionsService.removevote(this.submission.id).subscribe(
      (response) => { },
      (error) => { }
    );
  }

  onCommentQuoted(comment: CommentPost): void {
    this.commentInput = `> ${comment.text}`;
  }
}
