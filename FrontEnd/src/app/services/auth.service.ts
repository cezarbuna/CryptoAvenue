import { Injectable } from '@angular/core';
import {JwtHelperService} from "@auth0/angular-jwt";
import { JwtModule } from '@auth0/angular-jwt';
import {BehaviorSubject, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedInSubject = new BehaviorSubject<boolean>(this.hasToken());

  loginStatus$ = this.loggedInSubject.asObservable();

  constructor() {}

  private hasToken(): boolean {
    return !!localStorage.getItem('token') && !!localStorage.getItem('userId');
  }

  login(userId: string, token: string): void {
    localStorage.setItem('token', token);
    localStorage.setItem('userId', userId);
    this.loggedInSubject.next(true);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    this.loggedInSubject.next(false);
  }

  isLoggedIn(): boolean {
    return this.loggedInSubject.value;
  }
}
