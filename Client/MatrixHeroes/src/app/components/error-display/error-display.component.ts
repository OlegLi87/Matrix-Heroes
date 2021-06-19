import { Subject } from 'rxjs';
import { Component, Inject, OnInit } from '@angular/core';
import { ERROR_STREAM } from 'src/app/di_providers/errorStream.provider';
import { AppError, ErrorLevel } from 'src/app/models/appError.model';

@Component({
  selector: 'error-display',
  templateUrl: './error-display.component.html',
  styleUrls: ['./error-display.component.sass'],
})
export class ErrorDisplayComponent implements OnInit {
  errorMessage: string;
  isFatal: boolean;

  constructor(@Inject(ERROR_STREAM) private $errorStream: Subject<AppError>) {}

  ngOnInit(): void {
    this.$errorStream.subscribe(this.displayError.bind(this));
  }

  private displayError(error: AppError): void {
    this.errorMessage = error.message;
    this.isFatal = error.errorLevel === ErrorLevel.FATAL;

    setTimeout(() => {
      this.errorMessage = null;
      this.isFatal = false;
    }, 3000);
  }

  get backgroundColor(): string {
    return this.isFatal ? '#f70f0f' : '#f7b50f';
  }
}
