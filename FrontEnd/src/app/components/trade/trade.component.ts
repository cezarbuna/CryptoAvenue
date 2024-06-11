import { Component, HostListener, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CoinsService } from '../../services/coins.service';
import { WalletService } from '../../services/wallet.service';
import { CoinOption } from '../../models/CoinOption';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-trade',
  standalone: true,
  imports: [
    ButtonModule,
    CardModule,
    InputTextModule,
    MessageModule,
    NgIf,
    ReactiveFormsModule,
    NgClass,
    NgForOf,
    NgOptimizedImage
  ],
  templateUrl: './trade.component.html',
  styleUrls: ['./trade.component.css']
})
export class TradeComponent implements OnInit {
  tradeForm: FormGroup = new FormGroup({
    sourceCoin: new FormControl(null, Validators.required),
    targetCoin: new FormControl(null, Validators.required),
    sourceQuantity: new FormControl('', [Validators.required, Validators.min(0.01)]),
    targetQuantity: new FormControl('', [Validators.required, Validators.min(0.01)]),
    userId: new FormControl(localStorage.getItem('userId')),
  });

  selectedSourceCoin: CoinOption | undefined;
  selectedTargetCoin: CoinOption | undefined;
  sourceCoins: CoinOption[] = [];
  targetCoins: CoinOption[] = [];
  availableQuantity: number | null = null;
  availableQuantityAsString: string | null = null;
  maximumQuantity: number | null = null;
  maximumQuantityAsString: string | null = null;
  firstCoinSelected: boolean = false;
  dropdownOpen: { [key: string]: boolean } = { sourceCoin: false, targetCoin: false };

  constructor(
    private coinsService: CoinsService,
    private walletService: WalletService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const userId = this.tradeForm.value.userId;

    // Fetch available source coins for the user
    this.coinsService.getAllCoinsByUserId(userId).subscribe(
      res => {
        this.sourceCoins = res.map(coin => ({
          label: coin.name,
          value: coin.id,
          imageUrl: coin.imageUrl
        }));
        console.log(this.sourceCoins);
      },
      err => console.log(err)
    );

    // Update available quantity when source coin changes
    this.tradeForm.get('sourceCoin')?.valueChanges.subscribe(coinId => {
      if (coinId && userId) {
        this.coinsService.getAvailableQuantity(coinId, userId).subscribe(
          quantity => {
            this.availableQuantity = quantity;
            this.availableQuantityAsString = this.availableQuantity.toFixed(2);
            this.firstCoinSelected = true;

            // Fetch available target coins once a source coin is selected
            this.coinsService.getAllCoins().subscribe(
              res => {
                this.targetCoins = res.map(coin => ({
                  label: coin.name,
                  value: coin.id,
                  imageUrl: coin.imageUrl
                }));
              },
              err => console.log(err)
            );
          },
          err => console.log(err)
        );
      }
    });

    // Predict target quantity based on source quantity and selected coins
    this.tradeForm.get('sourceQuantity')?.valueChanges.subscribe(amount => {
      if (this.tradeForm.get('sourceQuantity')?.valid) {
        this.coinsService.predictAmount(
          userId,
          amount,
          this.tradeForm.get('sourceCoin')?.value,
          this.tradeForm.get('targetCoin')?.value
        ).subscribe(res => {
          this.tradeForm.get('targetQuantity')?.setValue(res.toFixed(2));
        });
      }
    });
  }

  // Toggle the dropdown open/close state
  toggleDropdown(type: 'sourceCoin' | 'targetCoin'): void {
    this.dropdownOpen[type] = !this.dropdownOpen[type];
  }

  // Select a coin from the dropdown
  selectCoin(coin: CoinOption, type: 'sourceCoin' | 'targetCoin', event: Event): void {
    event.stopPropagation(); // Prevent dropdown from closing immediately
    if (type === 'sourceCoin') {
      this.selectedSourceCoin = coin;
      this.tradeForm.patchValue({ sourceCoin: coin.value });

      const userId = this.tradeForm.get('userId')?.value;
      if (coin.value && userId) {
        this.coinsService.getAvailableQuantity(coin.value, userId).subscribe(
          quantity => {
            this.availableQuantity = quantity;
            this.availableQuantityAsString = this.availableQuantity.toFixed(2);
            this.firstCoinSelected = true;
          },
          err => console.log(err)
        );
      }
    } else if (type === 'targetCoin') {
      this.selectedTargetCoin = coin;
      this.tradeForm.patchValue({ targetCoin: coin.value });

      if (this.availableQuantity != null) {
        this.coinsService.predictAmount(
          this.tradeForm.get('userId')?.value,
          this.availableQuantity,
          this.tradeForm.get('sourceCoin')?.value,
          this.tradeForm.get('targetCoin')?.value
        ).subscribe(res => {
          this.maximumQuantity = res;
          this.maximumQuantityAsString = this.maximumQuantity.toFixed(2);
        });
      }
    }

    // Close the dropdown after selection
    this.dropdownOpen[type] = false;
  }

  // Listen for clicks outside the dropdown to close it
  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent): void {
    const sourceDropdown = (event.target as HTMLElement).closest('.custom-dropdown-source');
    const targetDropdown = (event.target as HTMLElement).closest('.custom-dropdown-target');

    if (this.dropdownOpen['sourceCoin'] && !sourceDropdown) {
      this.dropdownOpen['sourceCoin'] = false;
    }

    if (this.dropdownOpen['targetCoin'] && !targetDropdown) {
      this.dropdownOpen['targetCoin'] = false;
    }
  }

  // Get error message for form control validation
  getErrorMessage(controlName: string): string | null {
    const control = this.tradeForm.get(controlName);

    if (control && control.touched) {
      if (control.errors?.['required']) {
        return 'This field is required';
      }
      if (control.errors?.['min']) {
        return 'The minimum amount is 0.01';
      }
      if (control.errors?.['max']) {
        if (controlName === 'sourceQuantity') {
          return `You cannot trade more than available: ${this.availableQuantityAsString}`;
        }
        if (controlName === 'targetQuantity') {
          return `You cannot trade more than the maximum: ${this.maximumQuantityAsString}`;
        }
      }
    }
    return null;
  }

  // Submit trade form
  trade(): void {
    const sourceQuantity = this.tradeForm.get('sourceQuantity')?.value;
    const targetQuantity = this.tradeForm.get('targetQuantity')?.value;

    // Validate source and target quantities
    if(this.availableQuantity)
    if (sourceQuantity > this.availableQuantity) {
      this.tradeForm.get('sourceQuantity')?.setErrors({ max: true });
    }
    if(this.maximumQuantity)
    if (targetQuantity > this.maximumQuantity) {
      this.tradeForm.get('targetQuantity')?.setErrors({ max: true });
    }

    if (this.tradeForm.valid) {
      this.walletService.trade(
        this.tradeForm.get('userId')?.value,
        this.tradeForm.get('sourceCoin')?.value,
        sourceQuantity,
        this.tradeForm.get('targetCoin')?.value,
        targetQuantity
      ).subscribe(
        res => {
          console.log(res);
          window.alert('Trade executed successfully!');
          this.router.navigate(['portfolio']);
        },
        err => console.error('Trade execution error:', err)
      );
    } else {
      this.tradeForm.markAllAsTouched(); // Mark all fields as touched to show validation errors
    }
  }
}
