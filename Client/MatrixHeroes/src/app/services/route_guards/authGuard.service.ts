import { BehaviorSubject } from 'rxjs';
import { USER_STREAM } from '../../di_providers/userStream.provider';
import { Inject, Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
} from '@angular/router';
import { User } from '../../models/user.model';

@Injectable({ providedIn: 'root' })
export class AuthGuardService implements CanActivate {
  constructor(
    @Inject(USER_STREAM) private userStream$: BehaviorSubject<User | null>
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    if (
      (state.url.includes('login') && this.userStream$.value) ||
      (!state.url.includes('login') && !this.userStream$.value)
    )
      return false;

    return true;
  }
}
