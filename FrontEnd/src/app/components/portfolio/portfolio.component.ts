import { Component, OnInit } from '@angular/core';
import { WalletService } from "../../services/wallet.service";
import { Router } from "@angular/router";
import { CardModule } from "primeng/card";
import { TableModule } from "primeng/table";
import { CurrencyPipe, DecimalPipe, NgClass, NgIf, PercentPipe } from "@angular/common";
import { ChartModule } from "primeng/chart";
import {ButtonModule} from "primeng/button";

@Component({
  selector: 'app-portfolio',
  standalone: true,
  imports: [
    CardModule,
    TableModule,
    NgClass,
    CurrencyPipe,
    PercentPipe,
    DecimalPipe,
    NgIf,
    ChartModule,
    ButtonModule
  ],
  templateUrl: './portfolio.component.html',
  styleUrl: './portfolio.component.css'
})
export class PortfolioComponent implements OnInit {
  constructor(private walletService: WalletService,
              private router: Router) {}

  userId: string | null = null;
  wallet: any;
  walletBalance: number = 0;
  portfolioInfo: any[] = [];
  portfolioHistory: any[] = [];
  chartData: any;
  chartOptions: any;
  isChartVisible: boolean = true;

  ngOnInit(): void {
    this.userId = localStorage.getItem('userId');
    if (this.userId != null) {
      this.walletService.getWalletByUserId(this.userId).subscribe(wallet => {
        this.wallet = wallet;
        this.walletBalance = wallet.balance;
        this.walletService.getPortfolioInformation(this.wallet.id).subscribe(res => {
          this.portfolioInfo = res;
        });
        this.fetchPortfolioHistory(1); // Default to 24 hours
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

  onRowSelect(coinId: string): void {
    this.router.navigate(['/coin-detail', coinId]);
  }
}
