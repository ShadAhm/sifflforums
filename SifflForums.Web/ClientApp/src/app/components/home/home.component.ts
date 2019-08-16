import { Component, OnInit } from '@angular/core';
import { SubmissionsService } from '../../services/submissions.service';
import { Submission } from '../../models/comments';
import { PaginatedResult } from '../../models/pagination';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  submissions: PaginatedResult<Submission>;
  canNavigateNext: boolean;
  canNavigatePrevious: boolean;
  pageNumber: number = 1;
  pageSize: number = 20; 

  constructor(private submissionsService: SubmissionsService) { }

  ngOnInit() {
    this.getSubmissions(this.pageNumber, this.pageSize);
  }

  getSubmissions(pageNumber: number, pageSize: number): void {
    this.submissionsService.getSubmissions(pageNumber,pageSize).subscribe(
      (response: PaginatedResult<Submission>) => {
        this.submissions = response;
        this.bindPageNumbers();
      },
      (error) => { console.error("Error happened", error) }
    );
  }

  bindPageNumbers(): void {
    this.canNavigatePrevious = this.submissions.pageIndex > 1;
    this.canNavigateNext = this.submissions.pageIndex < this.submissions.totalPages; 
  }

  navigateNext(): void {
    this.getSubmissions(++this.pageNumber, this.pageSize); 
  }

  navigatePrevious(): void {
    this.getSubmissions(--this.pageNumber, this.pageSize);
  }
}
