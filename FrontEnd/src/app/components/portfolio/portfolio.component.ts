import { Component, OnInit } from '@angular/core';
import { WalletService } from "../../services/wallet.service";
import { Router } from "@angular/router";
import { CurrencyPipe, DecimalPipe, NgClass, NgForOf, NgIf, PercentPipe } from "@angular/common";
import {ButtonModule} from "primeng/button";
import {ChartModule} from "primeng/chart";

@Component({
  selector: 'app-portfolio',
  standalone: true,
  imports: [
    NgClass,
    CurrencyPipe,
    PercentPipe,
    DecimalPipe,
    NgIf,
    NgForOf,
    ButtonModule,
    ChartModule,
  ],
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.css']
})
export class PortfolioComponent implements OnInit {
  constructor(private walletService: WalletService,
              private router: Router) {}

  userId: string | null = null;
  userEmail: string | null = null;
  wallet: any;
  walletBalance: number = 0;
  portfolioInfo: any[] = [];
  portfolioHistory: any[] = [];
  chartData: any;
  chartOptions: any;
  isChartVisible: boolean = true;
  portfolioChange24h: number = 0;
  balanceVisible: boolean = true; // To track balance visibility

  ngOnInit(): void {
    this.userId = localStorage.getItem('userId');
    this.userEmail = localStorage.getItem('userEmail'); // Assuming the email is stored in localStorage
    if (this.userId != null) {
      this.walletService.getWalletByUserId(this.userId).subscribe(wallet => {
        this.wallet = wallet;
        this.walletBalance = wallet.balance;
        this.walletService.getPortfolioInformation(this.wallet.id).subscribe(res => {
          this.portfolioInfo = res;
        });
        this.fetchPortfolioHistory(1);
        this.fetchPortfolioChangePercentage24h(this.wallet.id);
      });
    }
  }

  fetchPortfolioHistory(days: number): void {
    if (this.wallet) {
      this.walletService.getPortfolioHistory(this.wallet.id, days).subscribe(history => {
        this.portfolioHistory = history;
        this.updateChartData();
      });
    }
  }

  navigateTo(page: string): void {
    this.router.navigate([`/${page}`]);
  }

  navigateToTrade(): void {
    this.router.navigate(['/trade']);
  }

  fetchPortfolioChangePercentage24h(walletId: string): void {
    if (this.wallet) {
      this.walletService.getPortfolioChange24h(walletId).subscribe(res => {
        this.portfolioChange24h = res;
        console.log(this.portfolioChange24h);
      })
    }
  }

  updateChartData(): void {
    this.chartData = {
      labels: this.portfolioHistory.map(item => new Date(item.timeStamp).toLocaleTimeString()), // Show only time
      datasets: [
        {
          label: 'Portfolio Value',
          data: this.portfolioHistory.map(item => item.totalValue),
          fill: false,
          borderColor: '#42A5F5',
          tension: 0.4 // Smooth the line
        }
      ]
    };

    this.chartOptions = {
      responsive: true,
      plugins: {
        legend: {
          display: true,
          position: 'top'
        }
      },
      scales: {
        x: {
          display: true,
          title: {
            display: true,
            text: 'Time'
          }
        },
        y: {
          display: true,
          title: {
            display: true,
            text: 'Value'
          }
        }
      }
    };
  }

  toggleBalanceVisibility(): void {
    this.balanceVisible = !this.balanceVisible;
  }
}
