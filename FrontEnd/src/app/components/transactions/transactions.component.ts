import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {TransactionService} from "../../services/transaction.service";
import {TransactionDto} from "../../models/TransactionDto";
import {ButtonModule} from "primeng/button";
import {TagModule} from "primeng/tag";
import {DataViewModule} from "primeng/dataview";
import {NgClass, NgForOf} from "@angular/common";
import {error} from "@angular/compiler-cli/src/transformers/util";
import {ConfirmationService, MessageService} from "primeng/api";

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [
    ButtonModule,
    TagModule,
    DataViewModule,
    NgClass,
    NgForOf
  ],
  providers: [ConfirmationService, MessageService],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements OnInit{
  constructor(private router: Router,
              private transactionsService: TransactionService,
              private confirmationService: ConfirmationService) {
  }
  transactions: TransactionDto[] = [];
  userId: string | null = null;
  ngOnInit(): void {
    this.userId = localStorage.getItem('userId');
    this.fetchTransactions();

  }
  fetchTransactions(): void {
    if(this.userId)
    this.transactionsService.getTransactions(this.userId).subscribe(
      res =>{
        this.transactions = res;
      }
    )
  }
  revertTransaction(transactionId: string): void {

    if(transactionId){
      this.transactionsService.revertTransaction(transactionId).subscribe(res => {
        console.log(res);
        this.fetchTransactions();
        window.alert("Transaction was reverted successfully!");
      },
        error => {
        console.log(error);
        window.alert("Error!");
        })
    }
  }
  deleteTransaction(transactionId: string): void {
    // this.confirmationService.confirm({
    //   message: 'Are you sure you want to delete this transaction?',
    //   header: 'Confirm',
    //   icon: 'pi pi-exclamation',
    //   accept: () => {
    //
    //   }
    // })
    if(transactionId){
      this.transactionsService.deleteTransaction(transactionId).subscribe(res => {
          console.log(res);
          this.fetchTransactions();
        },
        error => {
          console.log(error);
          window.alert("Error!");
        })
    }
  }

}
