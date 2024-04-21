import { Injectable } from '@angular/core';
import {JwtHelperService} from "@auth0/angular-jwt";
import { JwtModule } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(public jwtHelper: JwtHelperService) { }

  public isTokenExpired(token: string): boolean {
    console.log('Is token expired:', this.jwtHelper.isTokenExpired(token));
    return this.jwtHelper.isTokenExpired(token);
  }
}
