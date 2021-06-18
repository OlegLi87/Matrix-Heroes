import { Ability } from './../../models/ability.model';
import { HeroesRepositoryService } from './../../services/repositories/heroesRepository.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CustomFormValidatorService } from 'src/app/services/customFormValidator.service';

@Component({
  selector: 'hero-form',
  templateUrl: './hero-form.component.html',
  styleUrls: ['./hero-form.component.sass'],
  providers: [CustomFormValidatorService],
})
export class HeroFormComponent implements OnInit, OnDestroy {
  private subs: Subscription;
  isFormSubmitted = false;
  heroForm: FormGroup;
  abilities: Ability[] = [];

  constructor(
    private heroesRepo: HeroesRepositoryService,
    private router: Router,
    private validator: CustomFormValidatorService
  ) {}

  ngOnInit(): void {
    this.createForm();
    this.subs = this.heroesRepo.abilitiesStream$.subscribe((abilities) => {
      if (!abilities) return this.heroesRepo.getAbilities();
      this.abilities = [];
      abilities.forEach((ab) => this.abilities.push({ ...ab }));
    });
  }

  addColor(): void {
    const colorControl = new FormControl(null);
    this.colorsFormArray.push(colorControl);
  }

  removeColor(): void {
    this.colorsFormArray.removeAt(this.colorsFormArray.length - 1);
  }

  submit(): void {
    this.isFormSubmitted = true;
    if (this.heroForm.valid) {
      const data = this.heroForm.value;
      this.heroesRepo.createHero({ ...data }).then((res) => {
        if (res) this.router.navigate(['']);
      });
    }
  }

  get colorsFormArray(): FormArray {
    return this.heroForm.get('suitColors') as FormArray;
  }

  private createForm(): void {
    this.heroForm = new FormGroup({
      name: new FormControl(null, Validators.required),
      startingPower: new FormControl(null, [
        Validators.required,
        Validators.min(1),
      ]),
      abilityId: new FormControl(0, [this.validator.valueSelected]),
      suitColors: new FormArray([]),
    });
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }
}
