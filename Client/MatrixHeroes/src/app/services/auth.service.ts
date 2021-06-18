import { HttpService } from './http.service';
import { LocalStorageService } from './localStorage.service';
import { AppUtilsService } from './appUtils.service';
import { environment } from './../../environments/environment';
import { User } from '../models/user.model';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { USER_STREAM } from '../di_providers/userStream.provider';
import { UserCredentials } from '../models/userCredentials.model';

export enum LoginMode {
  SIGNIN,
  SIGNUP,
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private LOCAL_STORAGE_JWT_KEY = environment.localStorageJwtKey;

  constructor(
    private appUtils: AppUtilsService,
    private localStr: LocalStorageService,
    private httpService: HttpService,
    @Inject(USER_STREAM) private userStream$: BehaviorSubject<User | null>
  ) {}

  streamUserAtAppInit(): void {
    const token = this.localStr.getItem(this.LOCAL_STORAGE_JWT_KEY);
    if (!token) return this.userStream$.next(null);

    const user = this.appUtils.createUserFromToken(token);
    if (!user) this.localStr.removeItem(this.LOCAL_STORAGE_JWT_KEY);
    this.userStream$.next(user);
  }

  login(credentials: UserCredentials, mode: LoginMode): void {
    this.httpService
      .login(credentials, mode)
      .pipe(
        map((data) => this.appUtils.createUserFromToken(data.jwtToken)),
        tap((user) => {
          if (user)
            this.localStr.saveItem(this.LOCAL_STORAGE_JWT_KEY, user.token);
          this.userStream$.next(user);
        })
      )
      .subscribe();
  }

  logout(): void {
    this.localStr.removeItem(this.LOCAL_STORAGE_JWT_KEY);
    this.userStream$.next(null);
  }
}
