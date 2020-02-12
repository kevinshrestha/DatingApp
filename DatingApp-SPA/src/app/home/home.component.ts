import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  registerToggle() {
    this.registerMode = true;
  }

  // when this is active, we're calling cancel() from regsiter.component.ts to get false which is then passed up from our register
  // child component using the cancel register event in register.component.html
  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

}
