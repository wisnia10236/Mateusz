import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { JwtHelperService } from "@auth0/angular-jwt";
import { map } from "rxjs/operators";

@Injectable({
  // sluzy do wstrzykiwania do servisu naszego dekoratora
  providedIn: "root"
})
export class AuthService {
  baseUrl = "http://localhost:5000/api/auth/"; // adres bazowy dla loginu i hasla
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) {} // wstrzykujemy klienta aby miec do niego dostep

  login(model: any) {
    return this.http
      .post(this.baseUrl + "login", model) // pobiera odpowiedz ze strony dla authcontrollera na podstawie modelu
      .pipe(
        map((Response: any) => {
          const user = Response;
          if (user) {
            localStorage.setItem("token", user.token);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            console.log(this.decodedToken);
          }
        })
      ); // gdy dostajemy odpowiedz z jakas odpowiedzia (response) to zapisujemy ja i jesli jest to zapisujemy token
  }

  register(model: any) {
    return this.http.post(this.baseUrl + "register", model); // wysylanie dla api aby zarejestrowal go
  }

  loggedIn() {
    const token = localStorage.getItem("token");
    return !this.jwtHelper.isTokenExpired(token);
  }
}
// services nie jest automatycznie dodawany do app.module i trzeba go dopisac
