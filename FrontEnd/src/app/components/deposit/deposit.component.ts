import { Component, HostListener, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { WalletService } from '../../services/wallet.service';
import { Router } from '@angular/router';
import { NgClass, NgForOf, NgIf } from "@angular/common";
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgClass,
    NgIf,
    NgForOf,
    ToastModule // Import PrimeNG ToastModule
  ],
  providers: [MessageService], // Register the MessageService
  styleUrls: ['./deposit.component.css']
})
export class DepositComponent implements OnInit {
  depositForm: FormGroup = new FormGroup({
    quantity: new FormControl('', [Validators.required, Validators.min(200)]),
    currency: new FormControl('', [Validators.required]),
    userId: new FormControl(localStorage.getItem('userId')),
  });

  currencies = [
    { name: 'Euro', icon: 'pi pi-euro' },
    { name: 'US Dollar', icon: 'pi pi-dollar' }
  ];

  selectedCurrency: any;
  dropdownOpen = false;

  constructor(
    private http: HttpClient,
    private walletService: WalletService,
    private router: Router,
    private messageService: MessageService  // Inject the MessageService
  ) {}

  ngOnInit(): void {
    // Pre-select the first currency if needed
    this.selectedCurrency = this.currencies[1];
  }

  toggleDropdown(): void {
    this.dropdownOpen = !this.dropdownOpen;
  }

  selectCurrency(currency: any, event: Event): void {
    event.stopPropagation(); // Prevent the dropdown from closing immediately
    this.selectedCurrency = currency;
    this.depositForm.patchValue({ currency: currency.name });
    this.dropdownOpen = false;  // Close dropdown after selection
  }

  // Listen for clicks outside the dropdown to close it
  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent): void {
    if (this.dropdownOpen && !(event.target as HTMLElement).closest('.custom-dropdown')) {
      this.dropdownOpen = false;
    }
  }

  getErrorMessage(controlName: string): string | null {
    const control = this.depositForm.get(controlName);
    if (control && control.touched) {
      if (control.errors?.['required']) {
        return 'This field is required';
      }
      if (control.errors?.['min']) {
        return 'The minimum deposit is 200 Euros.';
      }
    }
    return null;
  }

  onSubmit(): void {
    if (this.depositForm.valid) {
      this.walletService.deposit(this.depositForm.value).subscribe({
        next: () => {
          this.showSuccess('Deposit done successfully');

          // Add a delay to allow the toast message to display
          setTimeout(() => {
            this.router.navigate(['portfolio']);
          }, 2000); // 2000 milliseconds = 2 seconds delay
        },
        error: (err) => {
          this.showError('Deposit failed. Please try again.');
          console.error('Deposit error:', err);
        }
      });
    } else {
      this.depositForm.markAllAsTouched(); // Mark all controls as touched to display validation messages
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
