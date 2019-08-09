import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Submission } from '../../models/comments';
import { SubmissionsService } from '../../services/submissions.service';

@Component({
  selector: 'app-submission-list-item',
  templateUrl: './submission-list-item.component.html',
  styles: []
})
export class SubmissionListItemComponent implements OnInit {
  @Input() model: Submission;

  constructor(private submissionsService: SubmissionsService) { }

  ngOnInit() {
  }

  upvote(): void {
    this.submissionsService.upvote(this.model.submissionId, this.model.votingBoxId).subscribe(
      (response) => { },
      (error) => { }
    );
  }

  downvote(): void {
    this.submissionsService.downvote(this.model.submissionId, this.model.votingBoxId).subscribe(
      (response) => { },
      (error) => { }
    );
  }
}
