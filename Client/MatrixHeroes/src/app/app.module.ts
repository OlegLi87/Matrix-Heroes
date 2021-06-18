import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { registerLocaleData } from '@angular/common';
import localeRu from '@angular/common/locales/ru';
import { CoreModule } from './core.module';

registerLocaleData(localeRu);

@NgModule({
  declarations: [AppComponent],
  imports: [CoreModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
