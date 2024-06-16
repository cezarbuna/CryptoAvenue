import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {DepositModel} from "../models/deposit-model";
import {map, Observable} from "rxjs";
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
  trade(userId: string, sourceCoinId: string, sourceQuantity: number, targetCoinId: string, targetQuantity: number): Observable<Wallet> {
    return this.httpClient.post<Wallet>(`https://localhost:7008/api/Wallets/trade/${userId}/${sourceCoinId}/${sourceQuantity}/${targetCoinId}/${targetQuantity}`, {})
  }
  getPortfolioInformation(walletId: string): Observable<any[]> {
    return this.httpClient.get<{$values: any[]}>(`https://localhost:7008/api/Wallets/get-portfolio-information/${walletId}`)
      .pipe(
        map(response => response.$values)
      );
  }
  getPortfolioHistory(walletId: string, days: number): Observable<any[]> {
    return this.httpClient.get<{$values: any[]}>(`https://localhost:7008/api/Wallets/get-portfolio-history/${walletId}/${days}`)
      .pipe(
        map(response => response.$values)
      );
  }
  getPortfolioChange24h(walletId: string): Observable<number> {
    return this.httpClient.get<number>(`https://localhost:7008/api/Wallets/get-portfolio-change-24h/${walletId}`);
  }

}
