import { catchError } from 'rxjs/operators';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, throwError } from 'rxjs';
import { USER_STREAM } from '../di_providers/userStream.provider';
import { AppError } from '../models/appError.model';
import { ERROR_STREAM } from '../di_providers/errorStream.provider';
import { User } from '../models/user.model';

@Injectable()
export class HttpInterceptorService implements HttpInterceptor {
  constructor(
    @Inject(ERROR_STREAM) private $errorStream: Subject<AppError>,
    @Inject(USER_STREAM) private $userStream: BehaviorSubject<User>
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.$userStream.value) {
      const token = this.$userStream.value.token;
      req = req.clone({
        headers: req.headers.append('Authorization', 'Bearer ' + token),
      });
    }

    return next.handle(req).pipe(
      catchError((err) => {
        let errorObj: AppError;
        if (err instanceof HttpErrorResponse) {
          if (!err.status)
            errorObj = new AppError('Unable to connect to server.', true);
          else if (err.status >= 500)
            errorObj = new AppError('Error occurred on the server', true);
          else errorObj = new AppError(err.error.errors, false);
        } else errorObj = new AppError('Something went wrong', true);

        this.$errorStream.next(errorObj);
        return throwError(err);
      })
    );
  }
}
