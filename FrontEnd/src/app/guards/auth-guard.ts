import {Injectable} from "@angular/core";
import {
  ActivatedRouteSnapshot,
  CanActivate,
  GuardResult,
  MaybeAsync,
  Router,
  RouterStateSnapshot
} from "@angular/router";
import {AuthService} from "../services/auth.service";

@Injectable({
  providedIn: 'root',
})

export class AuthGuard implements CanActivate {
  constructor( private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('token');
    const userId = localStorage.getItem('userId');

    if(token && userId) {
      return true;
    } else {
      this.router.navigate(['login']);
      return false;
    }
  }
}
