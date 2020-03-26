import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from 'protractor';
import { AlertifyService } from '../_services/alertify.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();  //emituje zdarzenie przycisku cancel

  model: any = {};

  constructor(private authService: AuthService, private alertify:AlertifyService) { }

  ngOnInit(): void {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {  //metoda rejestracji, wysylajace dane sa obserwowane wiec trzeba je zasubskrybowac no i pozniej to jakies wyswietlenie czy napewno sie udalo
      this.alertify.success('rejestracja udana');
    }, error => {
        this.alertify.error('wystapił błąd rejestracji');
    })
    
  }

  cancel() {
    this.cancelRegister.emit(false); //przekazujemy wartosc false(a moze byc cos byc)
  }

}
