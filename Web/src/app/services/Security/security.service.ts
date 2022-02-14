import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { User } from 'src/app/models/Security/user.model';
import { environment } from 'src/environments/environment';
import { LoginData } from 'src/app/models/Security/login-data.model';


@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  private token!: string;

  baseUrl = environment.baseUrl;

  seguridadCambio = new Subject<string>();

  private _user!: User;

  constructor(private router: Router, private http: HttpClient) { }

  public obtenerToken(): string {
    return this.token;
  }

  cargarUsuario(): void {
    const tokenBrowser = sessionStorage.getItem('token');
    if (!tokenBrowser) return;

    this.token = tokenBrowser;


    this.http.get<User>(this.baseUrl + 'usuario/').subscribe((response) => {
      //console.log('Login respuesta', response);
      this.token = response.token;
      this._user = {
        email: response.email,
        firstName: response.firstName,
        lastName: response.lastName,
        token: response.token,
        password: '',
        userName: response.userName,
        usuarioId: response.usuarioId,
      };
      this.seguridadCambio.next(response.userName);
      sessionStorage.setItem('token', response.token);
    });
  }

  registerUser(usr: User) {


    console.log('User', usr);

    this.http
      .post<User>(this.baseUrl + 'v1/account/register', usr)
      .subscribe((response) => {
        //console.log('Login respuesta', response);
        this.token = response.token;
        this._user = {
          email: response.email,
          firstName: response.firstName,
          lastName: response.lastName,
          token: response.token,
          password: '',
          userName: response.userName,
          usuarioId: response.usuarioId,
        }
        this.seguridadCambio.next(response.userName);
        sessionStorage.setItem('token', response.token);
        this.router.navigate(['/financialChat']);
      },
      (error) => {
        alert(error.error.Message)
      }
      );
  }

  login(loginData: LoginData) {
    this.http
      .post<User>(this.baseUrl + 'v1/account/login', loginData)
      .subscribe((response) => {
        console.log('Login respuesta', response);
        this.token = response.token;
        this._user = {
          email: response.email,
          firstName: response.firstName,
          lastName: response.lastName,
          token: response.token,
          password: '',
          userName: response.userName,
          usuarioId: response.usuarioId,
        },
        console.log(response.userName);
        this.seguridadCambio.next(response.userName);
        sessionStorage.setItem('token', response.token);
        this.router.navigate(['/financialChat']);
      },
        (error) => {
          alert(error.error.Message)
        }
      );
  }
  closeSesion() {
    //this._user = undefined;
    this.seguridadCambio.next("");
    sessionStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
  obtenerUsuario() {
    return { ...this._user };
  }
  onSesion() {
    return sessionStorage.getItem('token') != null;
  }
}
