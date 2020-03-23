import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService) { } // wstrzykniecie servisu do nawigacji

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.model).subscribe(next => { // jezeli nasza usluga logowania jest git to
      console.log('Zalogowales sie do aplikacji'); // wyswietli nam w logu takie cos 
    }, error => {
        console.log('wystapil blad logowania'); // jesli blad to wyswietli cos takiego
    }); 
  }

}
