import { AppUtilsService } from './appUtils.service';
import { catchError } from 'rxjs/operators';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { USER_STREAM } from '../di_providers/userStream.provider';
import { User } from '../models/user.model';

@Injectable()
export class HttpInterceptorService implements HttpInterceptor {
  constructor(
    private appUtils: AppUtilsService,
    @Inject(USER_STREAM) private userStream$: BehaviorSubject<User>
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.userStream$.value) {
      const token = this.userStream$.value.token;
      req = req.clone({
        headers: req.headers.append('Authorization', 'Bearer ' + token),
      });
    }

    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {
        let appError = this.appUtils.createAppError(err);
        return throwError(appError);
      })
    );
  }
}
