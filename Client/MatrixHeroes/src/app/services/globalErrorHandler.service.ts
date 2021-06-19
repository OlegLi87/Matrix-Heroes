import { ErrorLevel } from 'src/app/models/appError.model';
import { AuthService } from './auth.service';
import { Subject } from 'rxjs';
import { ERROR_STREAM } from './../di_providers/errorStream.provider';
import { ErrorHandler, Inject, Injectable, NgZone } from '@angular/core';
import { AppError } from '../models/appError.model';

@Injectable()
export class GlobalErrorHandlerService implements ErrorHandler {
  constructor(
    private authService: AuthService,
    private ngZone: NgZone,
    @Inject(ERROR_STREAM) private errorStream$: Subject<AppError>
  ) {}

  handleError(error: any): void {
    if (error instanceof AppError) {
      let funcToRun: () => void;
      if (error.statusCode === 401) funcToRun = () => this.authService.logout();
      else funcToRun = () => this.errorStream$.next(error);

      this.ngZone.run(funcToRun);
    } else {
      error = new AppError('Unkonown error occurred', 0, ErrorLevel.FATAL);
      this.errorStream$.next(error);
    }
  }
}
