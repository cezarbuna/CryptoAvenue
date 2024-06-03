import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {DepositModel} from "../models/deposit-model";
import {Observable} from "rxjs";
import {Wallet} from "../models/wallet";

@Injectable({
  providedIn: 'root'
})
export class WalletService {

  constructor(private httpClient: HttpClient) { }
  deposit(depositModel: DepositModel): Observable<Wallet> {
    return this.httpClient.post<Wallet>(`https://localhost:7008/api/Wallets`, depositModel);
  }
}
