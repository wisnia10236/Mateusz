import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from '@angular/compiler/src/util';
import { AlertifyService } from '../_services/alertify.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService, private alertify: AlertifyService) { } // wstrzykniecie servisu do nawigacji

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.model).subscribe(next => { // jezeli nasza usluga logowania jest git to
      this.alertify.success('Zalogowales sie do aplikacji'); // wyswietli nam w logu takie cos 
    }, error => {
        this.alertify.error('wystapil blad logowania'); // jesli blad to wyswietli cos takiego
    }); 
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logOut() {
    localStorage.removeItem('token'); // aby wylogowac sie to usuwamy token
    this.alertify.message('zostales wylogowany');
  }

}
