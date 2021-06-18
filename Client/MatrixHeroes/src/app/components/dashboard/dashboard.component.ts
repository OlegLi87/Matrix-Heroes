import { AuthService } from './../../services/auth.service';
import { User } from '../../models/user.model';
import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { USER_STREAM } from 'src/app/di_providers/userStream.provider';

@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass'],
})
export class DashboardComponent implements OnInit, OnDestroy {
  private sub: Subscription;
  activeUser: User;

  constructor(
    private authService: AuthService,
    @Inject(USER_STREAM) private userStream$: BehaviorSubject<User>
  ) {}

  ngOnInit(): void {
    this.sub = this.userStream$.subscribe((user) => {
      this.activeUser = user;
    });
  }

  logout(): void {
    this.authService.logout();
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
