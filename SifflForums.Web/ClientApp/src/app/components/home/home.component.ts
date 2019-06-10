import { Component, OnInit } from '@angular/core';
import { CommentThreadService } from '../../services/comment-thread.service';
import { CommentThread } from '../../models/comments';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: []
})
export class HomeComponent implements OnInit {
  commentThreads: CommentThread[];

  constructor(private commentThreadService: CommentThreadService) { }

  ngOnInit() {
    this.getThreads();
  }

  getThreads(): void {
    this.commentThreadService.getThreads().subscribe(
      (response: CommentThread[]) => { this.commentThreads = response },
      (error) => { console.error("Error happened", error) }
    );
  }
}
