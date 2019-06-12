import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { SubmissionsService } from '../../services/submissions.service';
import { Submission } from '../../models/comments';
import { Router } from '@angular/router';

@Component({
  selector: 'app-submission-create',
  templateUrl: './submission-create.component.html',
  styles: []
})
export class SubmissionCreateComponent implements OnInit {
  submissionForm : FormGroup = new FormGroup({
    title: new FormControl(''),
    text: new FormControl(''),
  });

  constructor(private router: Router, private submissionService: SubmissionsService) { }

  ngOnInit() {
  }

  onSubmit(): void {
    var input = new Submission();
    input.text = this.submissionForm.value.text;
    input.title = this.submissionForm.value.title;

    this.submissionService.postThread(input).subscribe(
      (response: Submission) => {
        this.router.navigateByUrl(`/submission/${response.submissionId}`);
      },
      (error) => { console.error("Error happened", error) }
    );
  }
}
