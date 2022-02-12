import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { SecurityService } from './security.service';


@Injectable()
export class SecurityRouter implements CanActivate {
  constructor(
    private securityService: SecurityService,
    private route: Router
  ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.securityService.onSesion()) return true;
    else {
      this.route.navigate(['/login']);
      return false;
    }
  }
}
