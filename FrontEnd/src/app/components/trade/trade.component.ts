import {Component, OnInit} from '@angular/core';
import {ButtonModule} from "primeng/button";
import {CardModule} from "primeng/card";
import {DropdownModule} from "primeng/dropdown";
import {InputTextModule} from "primeng/inputtext";
import {MessageModule} from "primeng/message";
import {NgIf, NgOptimizedImage} from "@angular/common";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {SharedModule} from "primeng/api";
import {UserServiceService} from "../../services/user-service.service";
import {CoinsService} from "../../services/coins.service";
import {Coin} from "../../models/Coin";
import {CoinOption} from "../../models/CoinOption";
import {max, min} from "rxjs";
import {WalletService} from "../../services/wallet.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-trade',
  standalone: true,
  imports: [
    ButtonModule,
    CardModule,
    DropdownModule,
    InputTextModule,
    MessageModule,
    NgIf,
    ReactiveFormsModule,
    SharedModule,
    NgOptimizedImage
  ],
  templateUrl: './trade.component.html',
  styleUrl: './trade.component.css'
})
export class TradeComponent implements OnInit{
  constructor(private coinsService: CoinsService,
              private walletService: WalletService,
              private router: Router) {}
  tradeForm: FormGroup =  new FormGroup({
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
  sourceCoinId: string = '';
  targetCoinId: string = '';

  ngOnInit(): void {
    this.coinsService.getAllCoinsByUserId(this.tradeForm.value.userId).subscribe(
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
    this.tradeForm.get('sourceCoin')?.valueChanges.subscribe(coinId => {
      const userId = this.tradeForm.get('userId')?.value;
      if(coinId && userId){
        this.coinsService.getAvailableQuantity(coinId, userId).subscribe(
          quantity => {
            this.availableQuantity = quantity;
          },
          err => console.log(err)
        )
      }
    });

    this.tradeForm.get('sourceQuantity')?.valueChanges.subscribe(amount => {
      if(this.tradeForm.get('sourceQuantity')?.valid){
        this.coinsService.predictAmount(
          this.tradeForm.get('userId')?.value,
          amount,
          // this.tradeForm.get('sourceCoin')?.value,
          this.sourceCoinId,
          this.targetCoinId).subscribe(res => {
          console.log('Calculated amount:');
          console.log(res);
          this.tradeForm.get('targetQuantity')?.setValue(res.toFixed(2));
        })
      }
    });

  }
  onCoinChange(coin: CoinOption): void {
    const userId = localStorage.getItem('userId') || '';
    console.log('Coin value:');
    console.log(coin);
    // this.tradeForm.get('sourceCoin')?.setValue(coin.value);
    this.sourceCoinId = coin.value;
    this.coinsService.getAvailableQuantity(coin.value, userId).subscribe(
      res => {
        this.availableQuantity = res;
        this.availableQuantityAsString = this.availableQuantity.toFixed(2);
        this.firstCoinSelected = true;
        this.selectedSourceCoin = coin;
        this.tradeForm.get('sourceQuantity')?.setValidators([Validators.max(res)]);
        this.coinsService.getAllCoins().subscribe(
          res => {
            this.targetCoins = res.map(coin => ({
              label: coin.name,
              value: coin.id,
              imageUrl: coin.imageUrl
            }));
          }
        )
      },
      err => console.log(err)
    );
  }
  onCoinChange2(coin: CoinOption): void {
    const userId = localStorage.getItem('userId') || '';
    // this.tradeForm.get('targetCoin')?.setValue(coin.value);
    this.targetCoinId = coin.value;
    this.selectedTargetCoin = coin;
    if(this.availableQuantity != null){
      this.coinsService.predictAmount(
        userId,
        this.availableQuantity,
        this.sourceCoinId,
        this.targetCoinId
      ).subscribe(res => {
        this.maximumQuantity = res;
        this.maximumQuantityAsString = this.maximumQuantity.toFixed(2);
      })
    }
  }

  trade(): void{
    // if(this.tradeForm.valid){
      this.walletService.trade(
        this.tradeForm.get('userId')?.value,
        this.sourceCoinId,
        this.tradeForm.get('sourceQuantity')?.value,
        this.targetCoinId,
        this.tradeForm.get('targetQuantity')?.value,
      ).subscribe(res => {
        console.log(res);
        window.alert('Deposit successful!');
        this.router.navigate(['home']);
      })
    // }
  }

}
