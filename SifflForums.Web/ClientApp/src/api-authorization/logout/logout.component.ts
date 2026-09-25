import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from '../authorize.service';
import { ApplicationPaths } from '../api-authorization.constants';

// Bearer tokens live only in the browser, so logging out just discards them.
@Component({
  selector: 'app-logout',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.Eager,
  template: '<p>Logging out...</p>'
})
export class LogoutComponent implements OnInit {
  constructor(
    private authorizeService: AuthorizeService,
    private router: Router) { }

  ngOnInit() {
    this.authorizeService.logout();
    this.router.navigateByUrl(ApplicationPaths.DefaultLoginRedirectPath, { replaceUrl: true });
  }
}
