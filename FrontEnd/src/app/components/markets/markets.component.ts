import { Component, OnInit } from '@angular/core';
import { TableModule } from "primeng/table";
import { Coin } from "../../models/Coin";
import { Router } from "@angular/router";
import { CoinsService } from "../../services/coins.service";
import { PaginatorModule } from "primeng/paginator";
import {CurrencyPipe, NgClass, PercentPipe} from "@angular/common";

@Component({
  selector: 'app-markets',
  standalone: true,
  imports: [
    TableModule,
    PaginatorModule,
    CurrencyPipe,
    NgClass,
    PercentPipe
  ],
  templateUrl: './markets.component.html',
  styleUrls: ['./markets.component.css']
})
export class MarketsComponent implements OnInit {
  coins: Coin[] = [];

  constructor(private router: Router, private coinService: CoinsService) { }

  ngOnInit(): void {
    this.coinService.getAllCoins().subscribe(
      res => {
        this.coins = res;
      }
    )
  }

  onRowSelect(coinId: string): void {
    this.router.navigate(['/coin-detail', coinId]);
  }
}
