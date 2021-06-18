import { AppError } from './../models/appError.model';
import { InjectionToken, Provider } from '@angular/core';
import { Subject } from 'rxjs';

export const ERROR_STREAM = new InjectionToken('Stream of app errors');

const errorStream$ = new Subject<AppError>();

export const erroStreamProvider: Provider = {
  provide: ERROR_STREAM,
  useValue: errorStream$,
};
