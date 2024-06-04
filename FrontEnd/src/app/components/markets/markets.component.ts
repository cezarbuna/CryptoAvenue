import {Component, OnInit} from '@angular/core';
import {TableModule} from "primeng/table";
import {Coin} from "../../models/Coin";
import {NgForOf, NgIf} from "@angular/common";
import {Router} from "@angular/router";
import {CoinsService} from "../../services/coins.service";
import {PaginatorModule} from "primeng/paginator";
import {CoinOption} from "../../models/CoinOption";

@Component({
  selector: 'app-markets',
  standalone: true,
  imports: [
    TableModule,
    NgForOf,
    NgIf,
    PaginatorModule
  ],
  templateUrl: './markets.component.html',
  styleUrl: './markets.component.css'
})
export class MarketsComponent implements OnInit{
  constructor(private router: Router,
              private coinService: CoinsService,){}

  coins: Coin[] = [];
  cols: any[] = [];
  sortField: string = 'name'; // Default sort field
  sortOrder: number = 1; // Default sort order
  ngOnInit(): void {
    this.coinService.getAllCoins().subscribe(
      res => {
        this.coins = res;
        console.log(this.coins);
      }
    )
    this.cols = [
      { field: 'id', header: 'ID' },
      { field: 'symbol', header: 'Symbol' },
      { field: 'name', header: 'Name' },
      { field: 'currentPrice', header: 'Current Price' },
      { field: 'marketCap', header: 'Market Cap' },
      { field: 'marketCapRank', header: 'Market Cap Rank' },
      { field: 'high24h', header: 'High 24h' },
      { field: 'low24h', header: 'Low 24h' },
      { field: 'priceChange24h', header: 'Price Change 24h' },
      { field: 'priceChangePercentage24h', header: 'Price Change % 24h' },
      { field: 'marketCapChange24h', header: 'Market Cap Change 24h' },
      { field: 'marketCapChangePercentage24h', header: 'Market Cap Change % 24h' },
      { field: 'ath', header: 'All Time High' },
      { field: 'imageUrl', header: 'Image' }
    ];
  }
  onRowSelect(coin: Coin): void {
    this.router.navigate(['/coin-detail', coin]);
  }

}
