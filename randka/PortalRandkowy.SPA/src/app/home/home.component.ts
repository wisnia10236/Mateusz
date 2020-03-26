import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  values: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getValues();
  }

  registerToggle() {
    this.registerMode = true;
  }

  cancelRegisterMode(registerMode : boolean) {
    this.registerMode = registerMode;
  }

  getValues() { // pobieranie wartosci z api dla angulara
    this.http.get('https://localhost:44340/api/Values').subscribe(Response => {
      this.values = Response;   // pobiera z localhost , odpowiedz(subscribe) zapisujemy do wartosci values
    }, error => { // wywolujemy error
      console.log(error);
    });
  }


}
