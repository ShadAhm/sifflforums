import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styles: []
})
export class HeaderComponent implements OnInit {
  isAuthenticated: boolean; 

  constructor(private router: Router, private authService: AuthService) {
    router.events.subscribe((val) => {
      this.checkAuthenticated(); 
    });
  }

  ngOnInit() {
  }

  checkAuthenticated(): void {
    this.isAuthenticated = this.authService.isSignedIn();
  }

  signOut(): void {
    this.authService.signOut();
    window.location.reload();
  }
}
