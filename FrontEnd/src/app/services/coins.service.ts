import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {Coin} from "../models/Coin";

@Injectable({
  providedIn: 'root'
})
export class CoinsService {

  constructor(private httpClient: HttpClient) { }
  // getAllCoins(): Observable<any[]> {
  //   return this.httpClient.get<any[]>(`https://localhost:7008/api/Coins/get-all-coins`);
  // }
  getAllCoins(): Observable<Coin[]> {
    return this.httpClient.get<{ $values: Coin[] }>(`https://localhost:7008/api/Coins/get-all-coins`)
      .pipe(
        map(response => response.$values)
      );
  }
  getAllCoinsByUserId(userId: string): Observable<Coin[]> {
    return this.httpClient.get<{ $values: Coin[] }>(`https://localhost:7008/api/Coins/get-user-coins/${userId}`)
      .pipe(
        map(response => response.$values)
      );
  }
}
