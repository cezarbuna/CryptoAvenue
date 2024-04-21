import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {HttpClientModule} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  constructor(private httpClient: HttpClient) { }

  validateEmail(email: string): Observable<boolean> {
    return this.httpClient.get<boolean>(`https://localhost:7008/api/Users/validate-user-email/${email}`);
  }
}
