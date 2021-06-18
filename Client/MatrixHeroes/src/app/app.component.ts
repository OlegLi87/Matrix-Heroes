import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';
import { USER_STREAM } from './di_providers/userStream.provider';

import { User } from './models/user.model';

@Component({
  selector: 'root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  constructor(
    private router: Router,
    @Inject(USER_STREAM) private userStream$: BehaviorSubject<User | null>
  ) {}

  ngOnInit(): void {
    this.userStream$.pipe(distinctUntilChanged()).subscribe((user) => {
      if (!user) this.router.navigate(['/login']);
      else this.router.navigate(['']);
    });
  }
}
