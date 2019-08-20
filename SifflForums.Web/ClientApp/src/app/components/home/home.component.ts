import { Component, OnInit } from '@angular/core';
import { ForumsectionsService } from '../../services/forumsections.service';
import { ForumSection } from '../../models/forums';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  forumSections: ForumSection[]; 

  constructor(private forumsectionsService: ForumsectionsService) { }

  ngOnInit() {
    this.getAllSections(); 
  }

  getAllSections(): void {
    this.forumsectionsService.getAll().subscribe(
      (response: ForumSection[]) => {
        this.forumSections = response;
      },
      (error) => { console.error("Error happened", error) }
    );
  }
}
