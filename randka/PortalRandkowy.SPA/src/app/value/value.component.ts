import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {

  values: any;  

  constructor(private http: HttpClient) { }// wstrzykiwanie

  ngOnInit(): void {
    this.getValues();
  }

  getValues() { // pobieranie wartosci z api dla angulara
    this.http.get('https://localhost:44340/api/Values').subscribe(Response => {
      this.values = Response;   // pobiera z localhost , odpowiedz(subscribe) zapisujemy do wartosci values
    }, error => { // wywolujemy error
      console.log(error);
    });
  }

}
