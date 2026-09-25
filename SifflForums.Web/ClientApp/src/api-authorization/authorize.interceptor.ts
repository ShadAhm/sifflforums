import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { AuthorizeService, SKIP_AUTHORIZATION } from './authorize.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeInterceptor implements HttpInterceptor {
  constructor(private authorize: AuthorizeService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.context.get(SKIP_AUTHORIZATION) || !this.isApiUrl(req)) {
      return next.handle(req);
    }

    return this.authorize.getAccessToken()
      .pipe(mergeMap(token => this.processRequestWithToken(token, req, next)));
  }

  // Adds the access_token (if any) to requests targeted at the API.
  private processRequestWithToken(token: string | null, req: HttpRequest<any>, next: HttpHandler) {
    if (!!token) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next.handle(req);
  }

  private isApiUrl(req: HttpRequest<any>): boolean {
    const apiRoot = environment.apiRootUrl;

    // The API is hosted on a different origin (e.g. https://localhost:44302/ in development)
    if (apiRoot) {
      return req.url.startsWith(apiRoot);
    }

    // The API is hosted on the same origin, so only relative urls target it
    return !/^([a-z][a-z0-9+.-]*:)?\/\//i.test(req.url);
  }
}
