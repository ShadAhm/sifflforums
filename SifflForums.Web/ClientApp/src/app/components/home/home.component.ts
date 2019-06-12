import { Component, OnInit } from '@angular/core';
import { SubmissionsService } from '../../services/submissions.service';
import { Submission } from '../../models/comments';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: []
})
export class HomeComponent implements OnInit {
  submissions: Submission[];

  constructor(private submissionsService: SubmissionsService) { }

  ngOnInit() {
    this.getSubmissions();
  }

  getSubmissions(): void {
    this.submissionsService.getThreads().subscribe(
      (response: Submission[]) => { this.submissions = response },
      (error) => { console.error("Error happened", error) }
    );
  }
}
