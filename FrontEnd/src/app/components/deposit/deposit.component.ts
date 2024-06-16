import { Component, HostListener, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { WalletService } from '../../services/wallet.service';
import { NgClass, NgForOf, NgIf } from "@angular/common";
import { MessageService } from "primeng/api";
import { loadStripe, Stripe } from '@stripe/stripe-js';
import { MessageModule } from "primeng/message";
import { ToastModule } from "primeng/toast";

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgClass,
    NgIf,
    NgForOf,
    MessageModule,
    ToastModule,
  ],
  providers: [MessageService],
  styleUrls: ['./deposit.component.css']
})
export class DepositComponent implements OnInit {

  depositForm: FormGroup = new FormGroup({
    quantity: new FormControl('', [Validators.required, Validators.min(200)]),
    currency: new FormControl('', [Validators.required]),
    userId: new FormControl(localStorage.getItem('userId')),
  });

  currencies = [
    { name: 'Euro', icon: 'pi pi-euro', priceId: '' },
    { name: 'US Dollar', icon: 'pi pi-dollar', priceId: '' }
  ];

  selectedCurrency: any;
  dropdownOpen = false;
  stripe: Stripe | null = null;

  constructor(private walletService: WalletService, private router: Router, private messageService: MessageService) {}

  async ngOnInit(): Promise<void> {
    this.stripe = await loadStripe('');

    this.selectedCurrency = this.currencies[0];
  }

  toggleDropdown(): void {
    this.dropdownOpen = !this.dropdownOpen;
  }

  selectCurrency(currency: any, event: Event): void {
    event.stopPropagation();
    this.selectedCurrency = currency;
    this.depositForm.patchValue({ currency: currency.name });
    this.dropdownOpen = false;
  }

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
      const userId = this.depositForm.get('userId')?.value;
      const amount = this.depositForm.get('quantity')?.value;
      const currency = this.selectedCurrency?.name;


      this.walletService.deposit(this.depositForm.value).subscribe({
        next: async (response) => {

          // this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Deposit registered successfully' });

          const priceId = this.selectedCurrency?.priceId;
          if (priceId && this.stripe) {
            const { error } = await this.stripe.redirectToCheckout({
              lineItems: [{ price: priceId, quantity: 1 }],
              mode: 'payment',
              successUrl: `http://localhost:4200/portfolio`,
              cancelUrl: `http://localhost:4200/portfolio`
            });

            if (error) {
              this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
            }
          } else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Currency not selected or Stripe not initialized' });
          }
        },
        error: (err) => {

          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to perform deposit. Please try again.' });
          console.error('Deposit error:', err);
        }
      });
    } else {
      this.depositForm.markAllAsTouched();
    }
  }
}
