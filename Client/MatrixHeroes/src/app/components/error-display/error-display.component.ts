import { Subject } from 'rxjs';
import { Component, Inject, OnInit } from '@angular/core';
import { ERROR_STREAM } from 'src/app/di_providers/errorStream.provider';
import { AppError } from 'src/app/models/appError.model';

@Component({
  selector: 'error-display',
  templateUrl: './error-display.component.html',
  styleUrls: ['./error-display.component.sass'],
})
export class ErrorDisplayComponent implements OnInit {
  errorMsg: string;
  isCritical: boolean;

  constructor(@Inject(ERROR_STREAM) private $errorStream: Subject<AppError>) {}

  ngOnInit(): void {
    this.$errorStream.subscribe((error) => {
      if (error) this.displayError(error);
    });
  }

  private displayError(error: AppError): void {
    this.errorMsg = error.message;
    this.isCritical = error.isCritical;

    setTimeout(() => {
      this.errorMsg = null;
      this.isCritical = false;
    }, 3000);
  }
}
