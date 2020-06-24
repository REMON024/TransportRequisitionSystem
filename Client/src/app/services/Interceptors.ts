import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptorService } from './auth-interceptor.service';
import { RequestInterceptorService } from './request-interceptor.service';


export const HTTPInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: RequestInterceptorService, multi: true }
];
