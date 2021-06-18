import { HeroesRepositoryService } from './../../services/repositories/heroesRepository.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Hero } from 'src/app/models/hero.model';
import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'heroes-list',
  templateUrl: './heroes-list.component.html',
  styleUrls: ['./heroes-list.component.sass'],
})
export class HeroesListComponent implements OnInit, OnDestroy {
  private subs: Subscription;
  heroes: Hero[] = [];

  constructor(private heroesRepo: HeroesRepositoryService) {}

  ngOnInit(): void {
    this.subs = this.heroesRepo.heroesStream$
      .pipe(
        map((heroes) =>
          heroes?.sort((h1, h2) => h2.currentPower - h1.currentPower)
        )
      )
      .subscribe((heroes) => {
        if (!heroes) return this.heroesRepo.getHeroes();
        this.heroes = [];
        heroes.forEach((h) => this.heroes.push({ ...h }));
      });
  }

  trackByFn(index: number, hero: Hero): string {
    return hero.id;
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }
}
