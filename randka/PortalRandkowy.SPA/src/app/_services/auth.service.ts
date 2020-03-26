import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({ //sluzy do wstrzykiwania do servisu naszego dekoratora
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'https://localhost:44340/api/auth/'; // adres bazowy dla loginu i hasla

  constructor(private http: HttpClient) { } // wstrzykujemy klienta aby miec do niego dostep

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model) // pobiera odpowiedz ze strony dla authcontrollera na podstawie modelu
      .pipe(map((Response: any) => { 
        const user = Response;
        if (user) {
          localStorage.setItem('token', user.token);
        }
      })); // gdy dostajemy odpowiedz z jakas odpowiedzia (response) to zapisujemy ja i jesli jest to zapisujemy token
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model); // wysylanie dla api aby zarejestrowal go
  }



}
// services nie jest automatycznie dodawany do app.module i trzeba go dopisac
