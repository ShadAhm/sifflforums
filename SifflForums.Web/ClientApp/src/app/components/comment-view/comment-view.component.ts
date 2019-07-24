import { Component, OnInit, Input } from '@angular/core';
import { CommentPost } from '../../models/comments';

@Component({
  selector: 'app-comment-view',
  templateUrl: './comment-view.component.html',
  styleUrls: ['./comment-view.component.scss']
})
export class CommentViewComponent implements OnInit {
  @Input() model: CommentPost;

  constructor() { }

  ngOnInit() {
  }
}
