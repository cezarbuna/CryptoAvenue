import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {HttpClientModule} from "@angular/common/http";
import {User} from "../models/user";
import {LoginModel} from "../models/login-model";
import {LoginResponse} from "../models/login-response";

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  constructor(private httpClient: HttpClient) { }

  validateEmail(email: string): Observable<boolean> {
    return this.httpClient.get<boolean>(`https://localhost:7008/api/Users/validate-user-email/${email}`);
  }

  loginUser(loginModel: LoginModel): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(`https://localhost:7008/api/Auth/login`, loginModel);
  }

  registerUser(user: User): Observable<User> {
    return this.httpClient.post<User>(`https://localhost:7008/api/Users`, user);
  }
}
