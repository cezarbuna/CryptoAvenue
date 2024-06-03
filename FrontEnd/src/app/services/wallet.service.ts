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
  getWalletByUserId(userId: string | null): Observable<Wallet> {
    return this.httpClient.get<Wallet>(`https://localhost:7008/api/Wallets/get-wallet-by-user-id/${userId}`);
  }
  withdraw(userId: string, quantity: number): Observable<Wallet> {
    return this.httpClient.patch<Wallet>(`https://localhost:7008/api/Wallets/withdraw/${userId}/${quantity}`, {});
  }
}
