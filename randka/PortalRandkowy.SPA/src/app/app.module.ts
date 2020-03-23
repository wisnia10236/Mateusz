import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http' // zaimportowanie modulu do naszego api
import { FormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';


@NgModule({
  declarations: [
    AppComponent,
    ValueComponent,
    NavComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule, // dodanie importu dla angulara aby widzial nasze api
    FormsModule,
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
