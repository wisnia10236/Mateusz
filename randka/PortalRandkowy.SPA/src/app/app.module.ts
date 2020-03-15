import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http' // zaimportowanie modulu do naszego api

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';

@NgModule({
  declarations: [
    AppComponent,
    ValueComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule // dodanie importu dla angulara aby widzial nasze api
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
