import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import {CurrencyPipe, DecimalPipe, NgClass, NgForOf} from "@angular/common";

interface Coin {
  id: string;
  name: string;
  symbol: string;
  currentPrice: number;
  marketCap: number;
  marketCapRank: number;
  high24h: number;
  low24h: number;
  priceChangePercentage24h: number;
  imageUrl: string;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    ButtonModule,
    NgClass,
    CurrencyPipe,
    DecimalPipe,
    NgForOf
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  trendingCoins: Coin[] = [];

  constructor(private router: Router, private httpClient: HttpClient) {}

  ngOnInit(): void {
    this.getTrendingCoins().subscribe(coins => {
      this.trendingCoins = coins.filter(coin =>
        ['bitcoin', 'ethereum', 'dogecoin', 'polkadot', 'shiba-inu'].includes(coin.id)
      );
    });
  }

  getTrendingCoins(): Observable<Coin[]> {
    return this.httpClient.get<{ $values: Coin[] }>(`https://localhost:7008/api/Coins/get-all-coins`)
      .pipe(
        map(response => response.$values)
      );
  }

  navigateToRegister(): void {
    this.router.navigate(['/register']);
  }

  navigateToTrade(): void {
    this.router.navigate(['/trade']);
  }
}
