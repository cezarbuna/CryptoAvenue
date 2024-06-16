import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { WalletService } from '../../services/wallet.service';
import { Router } from '@angular/router';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-withdraw',
  templateUrl: './withdraw.component.html',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgClass,
    NgIf,
    NgForOf,
    ToastModule  // Import PrimeNG ToastModule
  ],
  providers: [MessageService], // Register the MessageService
  styleUrls: ['./withdraw.component.css']
})
export class WithdrawComponent implements OnInit {
  withdrawForm: FormGroup = new FormGroup({
    quantity: new FormControl('', [Validators.required]),
    currency: new FormControl('', [Validators.required]),
    userId: new FormControl(localStorage.getItem('userId')),
  });

  currencies = [
    { name: 'Euro', icon: 'pi pi-euro' },
    { name: 'US Dollar', icon: 'pi pi-dollar' }
  ];

  selectedCurrency: any;
  dropdownOpen = false;
  private wallet: any;

  constructor(
    private http: HttpClient,
    private walletService: WalletService,
    private router: Router,
    private messageService: MessageService  // Inject the MessageService
  ) {}

  ngOnInit(): void {
    const userId = localStorage.getItem('userId');
    this.walletService.getWalletByUserId(userId).subscribe(
      res => {
        this.wallet = res;
        this.withdrawForm.get('quantity')?.setValidators([Validators.required, Validators.max(this.wallet.balance)]);
      }
    );
  }

  toggleDropdown(): void {
    this.dropdownOpen = !this.dropdownOpen;
  }

  selectCurrency(currency: any): void {
    this.selectedCurrency = currency;
    this.withdrawForm.patchValue({ currency: currency.name });
    setTimeout(() => {
      this.dropdownOpen = false;
    }, 100);  // Ensure the dropdown closes after the selection
  }

  getErrorMessage(controlName: string): string | null {
    const control = this.withdrawForm.get(controlName);

    if (control && control.touched) {
      if (control.errors?.['required']) {
        return 'This field is required';
      }
      if (control.errors?.['max']) {
        if (this.wallet) {
          return `Maximum amount available is ${this.wallet.balance}`;
        }
      }
    }
    return null;
  }

  onSubmit(): void {
    if (this.withdrawForm.valid) {
      this.walletService.withdraw(this.withdrawForm.value.userId, this.withdrawForm.value.quantity).subscribe({
        next: () => {
          this.showSuccess('Withdrawal done successfully');

          // Add a delay to allow the toast message to display
          setTimeout(() => {
            this.router.navigate(['home']);
          }, 2000); // 2000 milliseconds = 2 seconds delay
        },
        error: (err) => {
          this.showError('Withdrawal failed. Please try again.');
          console.error('Withdrawal error:', err);
        }
      });
    } else {
      this.withdrawForm.markAllAsTouched(); // Mark all controls as touched to display validation messages
    }
  }

  // Function to show success messages
  showSuccess(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: message });
  }

  // Function to show error messages
  showError(message: string) {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: message });
  }
}
