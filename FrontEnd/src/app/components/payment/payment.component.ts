import {Component, OnInit} from '@angular/core';
import {NgIf} from "@angular/common";
import {loadStripe, Stripe, StripeCardElement, StripeElements} from "@stripe/stripe-js";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit{
  stripe: Stripe | null = null;
  elements: StripeElements | null = null;
  card: StripeCardElement | null = null;
  clientSecret: string | null = null;

  constructor(private http: HttpClient) {}

  async ngOnInit(): Promise<void> {
    this.stripe = await loadStripe('pk_live_51PNK1tHMKY3In5GmxOEQubMudyreI266RTXdP5sFA6TxvqMq40OZzaQemUcVodWUqe2eF18WU0tJEHmqd5cx6I5H00CLcvDtPN');
  }

  initializePayment(): void {
    this.http.post<{ clientSecret: string }>('https://localhost:7008/api/Payments/create-payment-intent', { amount: 1000 })
      .subscribe({
        next: (data) => {
          this.clientSecret = data.clientSecret;
          console.log('Client Secret:', this.clientSecret); // Debugging line
          if (this.clientSecret) {
            this.initializeStripeElements();
          }
        },
        error: (err) => {
          console.error('Error creating payment intent:', err);
        }
      });
  }

  initializeStripeElements(): void {
    if (!this.stripe || !this.clientSecret) {
      console.error('Stripe or clientSecret not initialized');
      return;
    }

    this.elements = this.stripe.elements({ clientSecret: this.clientSecret });
    this.card = this.elements.create('card');

    if (!this.card) {
      console.error('Failed to create Stripe card element');
      return;
    }

    this.card.mount('#payment-element');
  }

  async submitPayment(): Promise<void> {
    if (!this.stripe || !this.elements || !this.clientSecret) {
      console.error('Stripe, elements, or clientSecret not initialized');
      return;
    }

    const { error, paymentIntent } = await this.stripe.confirmCardPayment(this.clientSecret, {
      payment_method: {
        card: this.card!,
      },
    });

    if (error) {
      console.error('Error confirming card payment:', error);
      alert(error.message);
    } else {
      alert('Payment successful!');
      console.log('Payment Intent:', paymentIntent);
    }
  }
}
