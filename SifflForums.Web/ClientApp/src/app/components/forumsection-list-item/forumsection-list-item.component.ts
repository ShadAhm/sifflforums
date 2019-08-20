import { Component, OnInit, Input } from '@angular/core';
import { SubmissionsService } from '../../services/submissions.service';
import { ForumSection } from '../../models/forums';
import { PaginatedResult } from '../../models/pagination';
import { Submission } from '../../models/comments';

@Component({
  selector: 'app-forumsection-list-item',
  templateUrl: './forumsection-list-item.component.html',
  styles: []
})
export class ForumsectionListItemComponent implements OnInit {
  @Input() model: ForumSection;
  submissions: PaginatedResult<Submission>;
  hasSubmissions: boolean; 

  constructor(private submissionsService: SubmissionsService) { }

  ngOnInit() {
    this.getSubmissions('Top', 1, 3); 
  }

  getSubmissions(selectedSorter: string, pageNumber: number, pageSize: number): void {
    this.submissionsService.getSubmissions(this.model.forumSectionId, selectedSorter, pageNumber, pageSize).subscribe(
      (response: PaginatedResult<Submission>) => {
        this.submissions = response;
        this.hasSubmissions = response != null && response.results != null && response.results.length > 0; 

      },
      (error) => { console.error("Error happened", error) }
    );
  }
}
