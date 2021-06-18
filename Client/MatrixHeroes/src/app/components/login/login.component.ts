import { AuthService } from './../../services/auth.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { LoginMode } from 'src/app/services/auth.service';
import { NgForm } from '@angular/forms';
import { UserCredentials } from 'src/app/models/userCredentials.model';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass'],
})
export class LoginComponent implements OnInit {
  @ViewChild('form') form: NgForm;

  mode: LoginMode = LoginMode.SIGNIN;
  credentials: UserCredentials = {
    userName: '',
    password: '',
    email: null,
  };

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  switchMode(): void {
    this.mode =
      this.mode === LoginMode.SIGNIN ? LoginMode.SIGNUP : LoginMode.SIGNIN;
    this.resetForm();
  }

  submit(): void {
    this.authService.login(this.credentials, this.mode);
  }

  private resetForm(): void {
    this.form?.reset();
    this.credentials.userName = '';
    this.credentials.password = '';
    this.credentials.email = null;
  }

  get modeText(): string {
    return this.mode === LoginMode.SIGNIN ? 'Sign In' : 'Sign Up';
  }

  get switchText(): string {
    return this.mode === LoginMode.SIGNIN ? 'New User?' : 'Existing User?';
  }

  get showEmail(): boolean {
    return this.mode === LoginMode.SIGNUP;
  }
}
