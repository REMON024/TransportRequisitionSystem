import { HttpRequest, HttpHandler } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenService } from './token.service';

@Injectable({
  providedIn : 'root'
})
export class AuthInterceptorService {

  constructor(private token: TokenService) {

  }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
   let authToken = '';

    if (this.token.GetToken() !== null) {
      authToken = this.token.GetToken();
    }

    const authReq = req.clone({
      headers: req.headers.set('Authorization', authToken)
    });

    return next.handle(authReq);
  }
}

