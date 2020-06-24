import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpResponse } from '@angular/common/http';
import { finalize, tap } from 'rxjs/operators';
import { NotificationService } from './notification.service';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';
import { ApiCommonMessage } from '../Models/ApiCommonMessage';
import { ErrorCode } from '../common/Enums/Enums';
import { LoaderService } from '../modules/loader/loader.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class RequestInterceptorService {

  private API_URL: string = environment.API_URL;

  constructor(private loaderService: LoaderService,
    private auth: AuthService,
    private notification: NotificationService,
    private route: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {

    const requestMessage =
      new ApiCommonMessage(this.auth.getLoggedUsername(),
        this.auth.getLoggedSessionId(), req.body);

    const customReq = req.clone({
      body: requestMessage,
      url: this.prepareUrl(req.url),
      headers: req.headers.set('Content-Type','application/json')
    });

    this.loaderService.show();
    return next.handle(customReq).pipe(
      tap(event => {
        if (event instanceof HttpResponse) {

        }
      }, error => {
        this.CheckAuthValidation(error);
        this.notification.dynamic(error);
      }),
      finalize(() => {
        this.loaderService.hide();
      }));
  }

  private CheckAuthValidation(error: any) {
    if (error.status === ErrorCode.UNAUTHENTICATE) {
        this.auth.RemoveToken();
        this.route.navigate(['login']);
    } else if (error.status === ErrorCode.FORBIDDEN) {
        this.route.navigate(['dashboard/forbidden']);
    }
  }

  private isAbsoluteUrl(url: string): boolean {
    const absolutePattern = /^https?:\/\//i;
    return absolutePattern.test(url);
  }

  private prepareUrl(url: string): string {
    url = this.isAbsoluteUrl(url) ? url : this.API_URL + '/' + url;
    return url.replace(/([^:]\/)\/+/g, '$1');
  }
}
