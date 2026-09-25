import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { AuthorizeService } from '../authorize.service';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-login-menu',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.Eager,
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.css']
})
export class LoginMenuComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;
  public isAdmin: Observable<boolean>;

  constructor(private authorizeService: AuthorizeService) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.isAdmin = this.authorizeService.isAdmin();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
  }
}
