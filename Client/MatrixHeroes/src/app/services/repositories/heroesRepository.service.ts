import { HttpService } from './../http.service';
import { Injectable } from '@angular/core';
import { Hero } from 'src/app/models/hero.model';
import { BehaviorSubject } from 'rxjs';
import { filter, tap } from 'rxjs/operators';
import { Ability } from 'src/app/models/ability.model';
import { NavigationEnd, Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class HeroesRepositoryService {
  private heroes: Hero[] = [];
  private abilities: Ability[] = [];
  readonly heroesStream$ = new BehaviorSubject<Hero[]>(null);
  readonly abilitiesStream$ = new BehaviorSubject<Ability[]>(null);

  constructor(private httpService: HttpService, router: Router) {
    router.events
      .pipe(
        filter(
          (e) =>
            e instanceof NavigationEnd &&
            router.routerState.snapshot.url === '/login'
        )
      )
      .subscribe(this.clearReposirotyState.bind(this));
  }

  getHeroes(): void {
    this.streamHeroes();
  }

  getAbilities(): void {
    this.streamAbilities();
  }

  createHero(hero: Hero): Promise<boolean> {
    return new Promise((res) => {
      this.httpService.saveHero(hero).subscribe((hero) => {
        this.heroes.push(hero);
        this.heroesStream$.next(this.heroes);
        res(true);
      });
    });
  }

  trainHero(hero: Hero): void {
    this.httpService.trainHero(hero.id).subscribe((hero) => {
      const index = this.heroes.findIndex((h) => h.id === hero.id);
      this.heroes.splice(index, 1, hero);
      this.heroesStream$.next(this.heroes);
    });
  }

  removeHero(hero: Hero): void {
    this.httpService.removeHero(hero.id).subscribe(() => {
      const index = this.heroes.findIndex((h) => h.id === hero.id);
      this.heroes.splice(index, 1);
      this.heroesStream$.next(this.heroes);
    });
  }

  private streamHeroes(): void {
    if (!this.heroes.length)
      this.httpService
        .fetchHeroes()
        .pipe(tap((heroes) => (this.heroes = heroes)))
        .subscribe((heroes) => this.heroesStream$.next(heroes));
    else this.heroesStream$.next(this.heroes);
  }

  private streamAbilities(): void {
    if (!this.abilities.length)
      this.httpService
        .fetchAbilities()
        .pipe(tap((abs) => (this.abilities = abs)))
        .subscribe((abilities) => this.abilitiesStream$.next(abilities));
    else this.abilitiesStream$.next(this.abilities);
  }

  private clearReposirotyState(): void {
    this.heroesStream$.next(null);
    this.abilitiesStream$.next(null);
    this.heroes = [];
    this.abilities = [];
  }
}
