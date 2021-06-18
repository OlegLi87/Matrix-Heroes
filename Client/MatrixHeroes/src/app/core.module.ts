import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ErrorDisplayComponent } from './components/error-display/error-display.component';
import { HeroCardComponent } from './components/hero-card/hero-card.component';
import { HeroFormComponent } from './components/hero-form/hero-form.component';
import { HeroesListComponent } from './components/heroes-list/heroes-list.component';
import { LoginComponent } from './components/login/login.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { appInitProvider } from './di_providers/appInit.provider';
import { erroStreamProvider } from './di_providers/errorStream.provider';
import { userStreamProvider } from './di_providers/userStream.provider';
import { TrainingStartDatePipe } from './pipes/trainingStart.pipe';
import { RoutingModule } from './routing.module';
import { HttpInterceptorService } from './services/httpInterceptor.service';

const declarations = [
  LoginComponent,
  DashboardComponent,
  HeroesListComponent,
  HeroCardComponent,
  NotFoundComponent,
  HeroFormComponent,
  ErrorDisplayComponent,
  TrainingStartDatePipe,
];

const imports = [
  BrowserModule,
  HttpClientModule,
  RoutingModule,
  FormsModule,
  ReactiveFormsModule,
];

@NgModule({
  declarations,
  imports,
  exports: [...declarations, ...imports],
  providers: [
    { provide: LOCALE_ID, useValue: 'ru-Ru' },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpInterceptorService,
      multi: true,
    },
    appInitProvider,
    userStreamProvider,
    erroStreamProvider,
  ],
})
export class CoreModule {}
