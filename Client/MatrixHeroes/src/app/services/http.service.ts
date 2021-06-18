import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginMode } from './auth.service';
import { Observable } from 'rxjs';
import { Hero } from '../models/hero.model';
import { Ability } from '../models/ability.model';
import { UserCredentials } from '../models/userCredentials.model';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private API_DOMAIN = environment.apiDomain;

  constructor(private httpClient: HttpClient) {}

  login(
    credentials: UserCredentials,
    mode: LoginMode
  ): Observable<{ jwtToken: string }> {
    const url =
      mode === LoginMode.SIGNIN
        ? this.API_DOMAIN + '/account/signin'
        : this.API_DOMAIN + '/account/signup';

    return this.httpClient.post<{ jwtToken: string }>(url, credentials);
  }

  fetchHeroes(): Observable<Hero[]> {
    return this.httpClient.get<Hero[]>(this.API_DOMAIN + '/heroes');
  }

  fetchAbilities(): Observable<Ability[]> {
    return this.httpClient.get<Ability[]>(this.API_DOMAIN + '/abilities');
  }

  saveHero(hero: Hero): Observable<Hero> {
    return this.httpClient.post<Hero>(this.API_DOMAIN + '/heroes', hero);
  }

  trainHero(id: string): Observable<Hero> {
    return this.httpClient.patch<Hero>(this.API_DOMAIN + `/heroes/${id}`, null);
  }

  removeHero(id: string): Observable<void> {
    return this.httpClient.delete<void>(this.API_DOMAIN + `/heroes/${id}`);
  }
}
