import { InjectionToken, Provider } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { User } from '../models/user.model';

export const USER_STREAM = new InjectionToken('User Stream');
const userStream$ = new BehaviorSubject<User>(null);

export const userStreamProvider: Provider = {
  provide: USER_STREAM,
  useValue: userStream$,
};
