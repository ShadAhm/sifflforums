import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { SubmissionsService } from '../../services/submissions.service';
import { Submission } from '../../models/comments';
import { Router, ActivatedRoute } from '@angular/router';
import { ForumsectionsService } from '../../services/forumsections.service';
import { ForumSection } from '../../models/forums';

@Component({
  selector: 'app-submission-create',
  templateUrl: './submission-create.component.html',
  styles: []
})
export class SubmissionCreateComponent implements OnInit {
  forumSection: ForumSection; 

  submissionForm : FormGroup = new FormGroup({
    title: new FormControl('', Validators.required),
    text: new FormControl('', Validators.required),
    forumSectionName: new FormControl('', Validators.required),
    forumSectionId: new FormControl('', Validators.required)
  });

  constructor(private router: Router, private route: ActivatedRoute, private submissionService: SubmissionsService, private forumSectionsService: ForumsectionsService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const forumSectionId = params['forumSectionId'];
      this.getForumSection(forumSectionId);
    });
  }

  getForumSection(forumSectionId: string): void {
    this.forumSectionsService.getById(forumSectionId).subscribe(
      (response: ForumSection) => {
        if (response != null) {
          this.submissionForm.controls.forumSectionName.setValue(response.name);
          this.submissionForm.controls.forumSectionId.setValue(response.id);
        }
      },
      (error) => { console.error("Error happened", error) }
    );
  }

  onSubmit(): void {
    if (!this.submissionForm.valid) {
      alert('Form invalid. All fields are required, please check.');
      return; 
    }

    var input = new Submission();
    input.text = this.submissionForm.value.text;
    input.title = this.submissionForm.value.title;
    input.forumSectionId = this.submissionForm.value.forumSectionId;

    this.submissionService.postSubmission(input).subscribe(
      (response: Submission) => {
        this.router.navigateByUrl(`/submission/${response.id}`);
      },
      (error) => { console.error("Error happened", error) }
    );
  }
}
