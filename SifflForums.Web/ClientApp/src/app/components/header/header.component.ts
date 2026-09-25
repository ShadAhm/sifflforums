import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.Eager,
  templateUrl: './header.component.html',
  styles: []
})
export class HeaderComponent implements OnInit {
  isAuthenticated: boolean; 
  loggedInUsername: string; 

  constructor(private router: Router) {
  }

  ngOnInit() {
  }
}
