import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SecurityService } from 'src/app/services/Security/security.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private securityService: SecurityService) { }

  ngOnInit(): void {
  }
  loginUsuario(form: NgForm) {
    this.securityService.login({
      email : form.value.email,
      password : form.value.password
    });
  }

}
