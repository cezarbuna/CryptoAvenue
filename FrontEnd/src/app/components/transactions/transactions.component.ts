import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { TransactionService } from "../../services/transaction.service";
import { TransactionDto } from "../../models/TransactionDto";
import { NgForOf, NgClass } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MessageService } from "primeng/api";
import { MessageModule } from "primeng/message";
import { ToastModule } from "primeng/toast";

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [
    NgForOf,
    NgClass,
    FormsModule,
    MessageModule,
    ToastModule,
  ],
  providers: [MessageService], // Provide MessageService
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit {
  transactions: TransactionDto[] = [];
  filteredTransactions: TransactionDto[] = [];
  filterOptions: string[] = ['All', 'Trade', 'Buy', 'Sell'];
  selectedFilter: string = 'All'; // Default filter

  userId: string | null = null;

  constructor(
    private router: Router,
    private transactionsService: TransactionService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.userId = localStorage.getItem('userId');
    this.fetchTransactions();
  }

  fetchTransactions(): void {
    if (this.userId) {
      this.transactionsService.getTransactions(this.userId).subscribe({
        next: res => {
          this.transactions = res;
          this.filterTransactions(); // Apply filter after fetching transactions
        },
        error: err => {
          console.log(err);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to fetch transactions.' });
        }
      });
    }
  }

  revertTransaction(transactionId: string): void {
    if (transactionId) {
      this.transactionsService.revertTransaction(transactionId).subscribe({
        next: res => {
          console.log(res);
          this.fetchTransactions();
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Transaction was reverted successfully!' });
        },
        error: err => {
          console.log(err);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to revert the transaction.' });
        }
      });
    }
  }

  deleteTransaction(transactionId: string): void {
    if (transactionId) {
      this.transactionsService.deleteTransaction(transactionId).subscribe({
        next: res => {
          console.log(res);
          this.fetchTransactions();
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Transaction was deleted successfully!' });
        },
        error: err => {
          console.log(err);
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete the transaction.' });
        }
      });
    }
  }

  getSeverity(type: string): string {
    switch (type) {
      case 'BUY':
        return 'success';
      case 'SELL':
        return 'warning';
      case 'TRADE':
        return 'info';
      default:
        return 'info';
    }
  }

  formatDate(date: string): string {
    const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return new Date(date).toLocaleDateString(undefined, options);
  }

  formatTime(time: string): string {
    const options: Intl.DateTimeFormatOptions = { hour: '2-digit', minute: '2-digit', second: '2-digit' };
    return new Date(`1970-01-01T${time}Z`).toLocaleTimeString(undefined, options);
  }

  filterTransactions(): void {
    if (this.selectedFilter === 'All') {
      this.filteredTransactions = this.transactions;
    } else {
      this.filteredTransactions = this.transactions.filter(transaction =>
        transaction.transactionType.toLowerCase() === this.selectedFilter.toLowerCase()
      );
    }
  }
}
