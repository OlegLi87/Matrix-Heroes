import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Hero } from '../models/hero.model';

@Pipe({
  name: 'trainStartDate',
})
export class TrainingStartDatePipe implements PipeTransform {
  transform(hero: Hero): string {
    const datePipe = new DatePipe('ru-Ru');
    const transformedDate = datePipe.transform(
      hero.trainingStartDate,
      'd/MM/yyyy'
    );
    if (transformedDate === '1/01/0001') return 'N/A';
    return transformedDate;
  }
}
