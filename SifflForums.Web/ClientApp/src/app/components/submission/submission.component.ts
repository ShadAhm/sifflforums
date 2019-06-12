import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SubmissionsService } from '../../services/submissions.service';
import { Submission, CommentPost } from '../../models/comments';
import { CommentsService } from '../../services/comments.service';


@Component({
  selector: 'app-submission',
  templateUrl: './submission.component.html',
  styles: []
})
export class SubmissionComponent implements OnInit {
  submissionId: number;
  submission: Submission;
  commentInput: string; 

  constructor(private route: ActivatedRoute, private commentsService: CommentsService, private submissionsService: SubmissionsService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.submissionId = params['submissionId'];
      this.getsubmissions(); 
    });
  }

  getsubmissions(): void {
    this.submissionsService.getSubmission(this.submissionId).subscribe(
      (response: Submission) => { this.submission = response },
      (error) => { console.error("Error happened", error) }
    );
  }

  postComment(): void {
    var input = new CommentPost();
    input.text = this.commentInput;
    input.submissionId = this.submissionId;

    this.commentsService.postComment(input).subscribe(
      (response) => { console.log('OK', response); },
      (error) => { console.error("Error happened", error) }
    );
  }
}
