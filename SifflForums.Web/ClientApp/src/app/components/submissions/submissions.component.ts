import { Component, OnInit } from '@angular/core';
import { SubmissionsService } from '../../services/submissions.service';
import { Submission } from '../../models/comments';
import { PaginatedResult } from '../../models/pagination';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-submissions',
  templateUrl: './submissions.component.html',
  styleUrls: ['./submissions.component.scss']
})
export class SubmissionsComponent implements OnInit {
  submissions: PaginatedResult<Submission[]>;
  canNavigateNext: boolean;
  canNavigatePrevious: boolean;
  pageNumber: number = 1;
  pageSize: number = 10;
  isEditingPageNumber: boolean;
  sortingOptions: string[] = ['New', 'Top'];
  selectedSorter: string = this.sortingOptions[0];
  forumSectionId: number; 

  constructor(private route: ActivatedRoute, private submissionsService: SubmissionsService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      var paramForumSectionId = params['forumSectionId'];
      var forumSectionId = parseInt(paramForumSectionId);

      if (forumSectionId != NaN) {
        this.forumSectionId = forumSectionId; 
        this.getSubmissions(this.selectedSorter, this.pageNumber, this.pageSize);
      }
    });
  }

  getSubmissions(selectedSorter: string, pageNumber: number, pageSize: number): void {
    this.submissionsService.getSubmissions(this.forumSectionId, selectedSorter, pageNumber, pageSize).subscribe(
      (response: PaginatedResult<Submission[]>) => {
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
    this.getSubmissions(this.selectedSorter, ++this.pageNumber, this.pageSize);
  }

  navigatePrevious(): void {
    this.getSubmissions(this.selectedSorter, --this.pageNumber, this.pageSize);
  }

  navigateTo(e): void {
    var navigateToPageNumber = e.currentTarget.valueAsNumber;

    if (navigateToPageNumber < 1 || navigateToPageNumber > this.submissions.totalPages) {
      alert('Sorry we can\'t do that');
      this.isEditingPageNumber = false;
      return;
    }

    this.pageNumber = navigateToPageNumber;
    this.getSubmissions(this.selectedSorter, this.pageNumber, this.pageSize);
    this.isEditingPageNumber = false;
  }

  selectedSorterChanged(e: string): void {
    this.getSubmissions(e, this.pageNumber, this.pageSize);
  }
}
