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

  seguridadCambio = new Subject<boolean>();

  private _user: User | undefined;

  constructor(private router: Router, private http: HttpClient) { }

  public obtenerToken(): string {
    return this.token;
  }

  cargarUsuario(): void {
    const tokenBrowser = sessionStorage.getItem('token');
    if (!tokenBrowser) return;

    this.token = tokenBrowser;
    this.seguridadCambio.next(true);

    this.http.get<User>(this.baseUrl + 'usuario/').subscribe((response) => {
      //console.log('Login respuesta', response);
      this.token = response.token;
      this._user = {
        email: response.email,
        firstName: response.firstName,
        lastName: response.lastName,
        token: response.token,
        password: '',
        username: response.username,
        usuarioId: response.usuarioId,
      };
      this.seguridadCambio.next(true);
      sessionStorage.setItem('token', response.token);
    });
  }

  registrarUsuario(usr: User) {


    console.log('User', usr);

    this.http
      .post<User>(this.baseUrl + 'usuario/registrar', usr)
      .subscribe((response) => {
        //console.log('Login respuesta', response);
        this.token = response.token;
        this._user = {
          email: response.email,
          firstName: response.firstName,
          lastName: response.lastName,
          token: response.token,
          password: '',
          username: response.username,
          usuarioId: response.usuarioId,
        };
        this.seguridadCambio.next(true);
        sessionStorage.setItem('token', response.token);
        this.router.navigate(['/']);
      });
  }

  login(loginData: LoginData) {
    this.http
      .post<User>(this.baseUrl + 'v1/account/login', loginData)
      .subscribe((response) => {
        //console.log('Login respuesta', response);
        this.token = response.token;
        this._user = {
          email: response.email,
          firstName: response.firstName,
          lastName: response.lastName,
          token: response.token,
          password: '',
          username: response.username,
          usuarioId: response.usuarioId,
        };
        this.seguridadCambio.next(true);
        sessionStorage.setItem('token', response.token);
        this.router.navigate(['/financialChat']);
      });
  }
  closeSesion() {
    this._user = undefined;
    this.seguridadCambio.next(false);
    sessionStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
  obtenerUsuario() {
    return {...this._user};
  }
  onSesion() {
    return sessionStorage.getItem('token') != null;
  }
}
