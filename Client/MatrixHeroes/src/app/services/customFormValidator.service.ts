import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Injectable()
export class CustomFormValidatorService {
  valueSelected(control: AbstractControl): { [key: string]: string } {
    if (!control.value)
      return {
        selectionError: 'value must be selected',
      };
    return null;
  }
}
