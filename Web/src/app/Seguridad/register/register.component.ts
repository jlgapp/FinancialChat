import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SecurityService } from 'src/app/services/Security/security.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private securityServices:SecurityService) { }

  ngOnInit(): void {
  }
  registerUser(form: NgForm) {
    this.securityServices.registerUser({
      email:form.value.email,
      password:form.value.password,
      lastName:form.value.lastName,
      firstName:form.value.firstName,
      userName:form.value.userName,
      usuarioId:'',
      token:''
    });
    console.log(form);
  }
}
