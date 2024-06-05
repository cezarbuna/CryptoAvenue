import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {CoinsService} from "../../services/coins.service";
import {ChartModule} from "primeng/chart";
import 'chartjs-adapter-date-fns';

@Component({
  selector: 'app-coin-detail',
  standalone: true,
  imports: [
    ChartModule
  ],
  templateUrl: './coin-detail.component.html',
  styleUrl: './coin-detail.component.css'
})
export class CoinDetailComponent implements OnInit{
  data: any;
  options: any;
  coinId: string | undefined;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private coinService: CoinsService) {
  }
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.coinId = params['coinId'];
      this.fetchCoinData();
    })
  }
  fetchCoinData(): void {
    // if(this.coinId)
    //   this.coinService.getCoinChart24hrs(this.coinId).subscribe(res => {
    //     console.log(res);
    //   })
    if(this.coinId){
      this.coinService.getCoinChart24hrs(this.coinId).subscribe(response => {
        const prices = response.prices.map((p: any) => ({ x: new Date(p[0]), y: p[1] }));
        const marketCaps = response.market_caps.map((m: any) => ({ x: new Date(m[0]), y: m[1] }));
        const volumes = response.total_volumes.map((v: any) => ({ x: new Date(v[0]), y: v[1] }));

        this.data = {
          datasets: [
            {
              label: 'Prices',
              data: prices,
              fill: false,
              borderColor: 'blue',
              tension: 0.4
            },
            {
              label: 'Market Caps',
              data: marketCaps,
              fill: false,
              borderColor: 'green',
              tension: 0.4
            },
            {
              label: 'Volumes',
              data: volumes,
              fill: false,
              borderColor: 'red',
              tension: 0.4
            }
          ]
        };

        const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');
        const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
        const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

        this.options = {
          maintainAspectRatio: false,
          aspectRatio: 0.6,
          plugins: {
            legend: {
              labels: {
                color: textColor
              }
            }
          },
          scales: {
            x: {
              type: 'time',
              time: {
                unit: 'hour'
              },
              ticks: {
                color: textColorSecondary
              },
              grid: {
                color: surfaceBorder,
                drawBorder: false
              }
            },
            y: {
              ticks: {
                color: textColorSecondary
              },
              grid: {
                color: surfaceBorder,
                drawBorder: false
              }
            }
          }
        };
      })
    }
  }

}
