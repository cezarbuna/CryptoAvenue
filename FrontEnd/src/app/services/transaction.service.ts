import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {TransactionDto} from "../models/TransactionDto";
import {map, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private httpClient: HttpClient) { }
  getTransactions(userId: string): Observable<TransactionDto[]>{
    return this.httpClient.get<{ $values: TransactionDto[] }>(`https://localhost:7008/api/Transactions/get-transactions-by-user-id/${userId}`)
      .pipe(
        map(response => response.$values)
      );
  }
  revertTransaction(transactionId: string): Observable<any>{
    return this.httpClient.patch<any>(`https://localhost:7008/api/Transactions/revert-transaction/${transactionId}`, {});
  }
  deleteTransaction(transactionId: string): Observable<any>{
    return this.httpClient.delete(`https://localhost:7008/api/Transactions/delete/${transactionId}`);
  }
}
