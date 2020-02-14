import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // Input receives data
  // Output emit events, sends data
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  // need to add auth service for register component
  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  // to use the register from auth service
  // we need to subscribe to it in our componennt
  register() {
    this.authService.register(this.model).subscribe(() => {
      this.alertify.success('registration successful');
    }, error => {
      this.alertify.error(error);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }

}
