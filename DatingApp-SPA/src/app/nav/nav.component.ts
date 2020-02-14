import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  // this will store username and password
  // we'll be able to pass to another method to send to server
  model: any = {};

  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('You have logged in successfully');
    }, error => {
      this.alertify.error(error);
    });
  }

  loggedIn() {
    // the tokens are being checked for its expiry date
    // this also means it's being check if it's valid token
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('You have been logged out');
  }
}
