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
  submissionId: number;
  submission: Submission;
  comments: CommentPost[];
  commentInput: string; 

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

        this.submission.upvotes = 43292; // temporary

        if (this.submission.submissionId > 0)
          this.getComments(this.submission.submissionId);
      },
      (error) => { console.error("Error happened", error) }
    );
  }

  getComments(parentId: number): void {
    this.commentsService.getComments(parentId).subscribe(
      (response: CommentPost[]) => { this.comments = response },
      (error) => { console.error("Error happened", error) }
    );
  }

  postComment(): void {
    var input = new CommentPost();
    input.text = this.commentInput;
    input.submissionId = this.submissionId;

    this.commentsService.postComment(input).subscribe(
      (response) => {
        this.comments.push(response); 
      },
      (error) => { console.error("Error happened", error) }
    );
  }

  upvote(): void {
    ++this.submission.upvotes;

    this.submissionsService.upvote(this.submission.submissionId).subscribe(
      (response) => { },
      (error) => { }
    );
  }

  downvote(): void {
    --this.submission.upvotes; 
  }
}
