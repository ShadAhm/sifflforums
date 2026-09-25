import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ForumsectionsService } from '../../services/forumsections.service';
import { ForumSection } from '../../models/forums';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.Eager,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  forumSections: ForumSection[];
  loaded: boolean;
  errorMessage: string;

  constructor(private router: Router, private forumsectionsService: ForumsectionsService) { }

  ngOnInit() {
    this.getAllSections();
  }

  getAllSections(): void {
    this.forumsectionsService.getAll().subscribe(
      (response: ForumSection[]) => {
        this.forumSections = response;
        this.loaded = true;

        if (this.forumSections.length == 1) {
          this.router.navigateByUrl(`/submissions/${this.forumSections[0].id}`);
        }
      },
      (error) => {
        console.error("Error happened", error);
        this.loaded = true;
        this.errorMessage = 'Unable to load forum sections.';
      }
    );
  }
}
