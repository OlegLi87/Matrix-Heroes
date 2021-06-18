import { HeroesRepositoryService } from './../../services/repositories/heroesRepository.service';
import { Component, Input } from '@angular/core';
import { Hero } from 'src/app/models/hero.model';

@Component({
  selector: 'hero-card',
  templateUrl: './hero-card.component.html',
  styleUrls: ['./hero-card.component.sass'],
})
export class HeroCardComponent {
  @Input() hero: Hero;

  constructor(private heroesRepo: HeroesRepositoryService) {}

  train(): void {
    this.heroesRepo.trainHero(this.hero);
  }

  remove(): void {
    this.heroesRepo.removeHero(this.hero);
  }
}
