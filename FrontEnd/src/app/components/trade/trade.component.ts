import {Component, OnInit} from '@angular/core';
import {ButtonModule} from "primeng/button";
import {CardModule} from "primeng/card";
import {DropdownModule} from "primeng/dropdown";
import {InputTextModule} from "primeng/inputtext";
import {MessageModule} from "primeng/message";
import {NgIf} from "@angular/common";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {SharedModule} from "primeng/api";
import {UserServiceService} from "../../services/user-service.service";
import {CoinsService} from "../../services/coins.service";
import {Coin} from "../../models/Coin";
import {CoinOption} from "../../models/CoinOption";

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
        SharedModule
    ],
  templateUrl: './trade.component.html',
  styleUrl: './trade.component.css'
})
export class TradeComponent implements OnInit{
  constructor(private coinsService: CoinsService) {}
  tradeForm: FormGroup =  new FormGroup({
    quantity: new FormControl('', [Validators.required]),
    userId: new FormControl(localStorage.getItem('userId')),
  });
  sourceCoins: CoinOption[] = [];

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
    )
  }

}
