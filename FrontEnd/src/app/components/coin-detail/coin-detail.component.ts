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
    if (this.coinId) {
      this.coinService.getCoinMarketChart(this.coinId, 'usd', 1).subscribe(response => {
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
              tension: 0.4,
              yAxisID: 'y1'
            },
            {
              label: 'Market Caps',
              data: marketCaps,
              fill: false,
              borderColor: 'green',
              tension: 0.4,
              yAxisID: 'y2'
            },
            {
              label: 'Volumes',
              data: volumes,
              fill: false,
              borderColor: 'red',
              tension: 0.4,
              yAxisID: 'y3'
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
              },
              onClick: (e: any, legendItem: { datasetIndex: any; }, legend: { chart: any; }) => {
                const index = legendItem.datasetIndex;
                const ci = legend.chart;
                const meta = ci.getDatasetMeta(index);

                // Toggle the visibility
                meta.hidden = meta.hidden === null ? !ci.data.datasets[index].hidden : null;

                // Update the chart
                ci.update();
                this.updateScales(ci);
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
                color: 'rgba(255, 255, 255, 0.1)',
                drawBorder: false
              }
            },
            y1: {
              display: true,
              position: 'left',
              ticks: {
                color: textColorSecondary,
                callback: (value: number) => `$${value.toFixed(2)}`
              },
              grid: {
                color: 'rgba(255, 255, 255, 0.1)',
                drawBorder: false
              }
            },
            y2: {
              display: false,
              position: 'right',
              ticks: {
                color: textColorSecondary,
                callback: (value: number) => this.formatLargeNumbers(value)
              },
              grid: {
                drawOnChartArea: false
              }
            },
            y3: {
              display: false,
              position: 'right',
              ticks: {
                color: textColorSecondary,
                callback: (value: number) => this.formatLargeNumbers(value)
              },
              grid: {
                drawOnChartArea: false
              }
            }
          }
        };
      });
    }
  }

  formatLargeNumbers(value: number): string {
    if (value >= 1e9) {
      return `${(value / 1e9).toFixed(2)} B`;
    } else if (value >= 1e6) {
      return `${(value / 1e6).toFixed(2)} M`;
    } else {
      return value.toString();
    }
  }

  updateScales(chart: any): void {
    const y1Visible = chart.isDatasetVisible(0);
    const y2Visible = chart.isDatasetVisible(1);
    const y3Visible = chart.isDatasetVisible(2);

    chart.options.scales.y1.display = y1Visible;
    chart.options.scales.y2.display = y2Visible;
    chart.options.scales.y3.display = y3Visible;

    chart.update();
  }
  // fetchCoinData(): void {
  //   if (this.coinId) {
  //     this.coinService.getCoinMarketChart(this.coinId, 'usd', 1).subscribe(response => {
  //       const prices = response.prices.map((p: any) => ({ x: new Date(p[0]), y: p[1] }));
  //       const marketCaps = response.market_caps.map((m: any) => ({ x: new Date(m[0]), y: m[1] }));
  //       const volumes = response.total_volumes.map((v: any) => ({ x: new Date(v[0]), y: v[1] }));
  //
  //       this.data = {
  //         datasets: [
  //           {
  //             label: 'Prices',
  //             data: prices,
  //             yAxisID: 'y1',
  //             fill: false,
  //             borderColor: 'blue',
  //             tension: 0.4
  //           },
  //           {
  //             label: 'Market Caps',
  //             data: marketCaps,
  //             yAxisID: 'y2',
  //             fill: false,
  //             borderColor: 'green',
  //             tension: 0.4
  //           },
  //           {
  //             label: 'Volumes',
  //             data: volumes,
  //             yAxisID: 'y3',
  //             fill: false,
  //             borderColor: 'red',
  //             tension: 0.4
  //           }
  //         ]
  //       };
  //
  //       const documentStyle = getComputedStyle(document.documentElement);
  //       const textColor = documentStyle.getPropertyValue('--text-color');
  //       const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
  //       const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
  //
  //       this.options = {
  //         maintainAspectRatio: false,
  //         aspectRatio: 0.6,
  //         plugins: {
  //           legend: {
  //             labels: {
  //               color: textColor
  //             }
  //           }
  //         },
  //         scales: {
  //           x: {
  //             type: 'time',
  //             time: {
  //               unit: 'hour'
  //             },
  //             ticks: {
  //               color: textColorSecondary
  //             },
  //             grid: {
  //               color: 'rgba(255, 255, 255, 0.1)',
  //               drawBorder: false
  //             }
  //           },
  //           y1: {
  //             type: 'linear',
  //             position: 'left',
  //             ticks: {
  //               color: textColorSecondary,
  //               callback: function (value: number) {
  //                 return `$${value.toFixed(2)}`;
  //               }
  //             },
  //             grid: {
  //               color: 'rgba(255, 255, 255, 0.1)',
  //               drawBorder: false
  //             }
  //           },
  //           y2: {
  //             type: 'linear',
  //             position: 'right',
  //             ticks: {
  //               color: textColorSecondary,
  //               callback: function (value: number) {
  //                 return value >= 1e9
  //                   ? `${(value / 1e9).toFixed(2)} B`
  //                   : value >= 1e6
  //                     ? `${(value / 1e6).toFixed(2)} M`
  //                     : value.toString();
  //               }
  //             },
  //             grid: {
  //               color: 'rgba(255, 255, 255, 0.1)',
  //               drawBorder: false
  //             }
  //           },
  //           y3: {
  //             type: 'linear',
  //             position: 'right',
  //             ticks: {
  //               color: textColorSecondary,
  //               callback: function (value: number) {
  //                 return value >= 1e9
  //                   ? `${(value / 1e9).toFixed(2)} B`
  //                   : value >= 1e6
  //                     ? `${(value / 1e6).toFixed(2)} M`
  //                     : value.toString();
  //               }
  //             },
  //             grid: {
  //               color: 'rgba(255, 255, 255, 0.1)',
  //               drawBorder: false
  //             }
  //           }
  //         }
  //       };
  //     })
  //   }
  // }
  // fetchCoinData(): void {
  //   // if(this.coinId)
  //   //   this.coinService.getCoinChart24hrs(this.coinId).subscribe(res => {
  //   //     console.log(res);
  //   //   })
  //   if(this.coinId){
  //     this.coinService.getCoinMarketChart(this.coinId, 'usd', 1).subscribe(response => {
  //       const prices = response.prices.map((p: any) => ({ x: new Date(p[0]), y: p[1] }));
  //       const marketCaps = response.market_caps.map((m: any) => ({ x: new Date(m[0]), y: m[1] }));
  //       const volumes = response.total_volumes.map((v: any) => ({ x: new Date(v[0]), y: v[1] }));
  //
  //       this.data = {
  //         datasets: [
  //           {
  //             label: 'Prices',
  //             data: prices,
  //             fill: false,
  //             borderColor: 'blue',
  //             tension: 0.4
  //           },
  //           {
  //             label: 'Market Caps',
  //             data: marketCaps,
  //             fill: false,
  //             borderColor: 'green',
  //             tension: 0.4
  //           },
  //           {
  //             label: 'Volumes',
  //             data: volumes,
  //             fill: false,
  //             borderColor: 'red',
  //             tension: 0.4
  //           }
  //         ]
  //       };
  //
  //       const documentStyle = getComputedStyle(document.documentElement);
  //       const textColor = documentStyle.getPropertyValue('--text-color');
  //       const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
  //       const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
  //
  //       this.options = {
  //         maintainAspectRatio: false,
  //         aspectRatio: 0.6,
  //         plugins: {
  //           legend: {
  //             labels: {
  //               color: textColor
  //             }
  //           }
  //         },
  //         scales: {
  //           x: {
  //             type: 'time',
  //             time: {
  //               unit: 'hour'
  //             },
  //             ticks: {
  //               color: textColorSecondary
  //             },
  //             grid: {
  //               color: 'rgba(255, 255, 255, 0.1)',
  //               drawBorder: false
  //             }
  //           },
  //           y: {
  //             ticks: {
  //               color: textColorSecondary
  //             },
  //             grid: {
  //               color: 'rgba(255, 255, 255, 0.1)',
  //               drawBorder: false
  //             }
  //           }
  //         }
  //       };
  //     })
  //   }
  // }

}
