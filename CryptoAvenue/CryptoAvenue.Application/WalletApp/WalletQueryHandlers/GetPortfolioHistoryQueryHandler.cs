using CryptoAvenue.Application.CoinDtos;
using CryptoAvenue.Application.Services;
using CryptoAvenue.Application.WalletApp.WalletQueries;
using CryptoAvenue.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletQueryHandlers
{
    public class GetPortfolioHistoryQueryHandler : IRequestHandler<GetPortfolioHistoryQuery, List<PortfolioHistoryDto>>
    {
        private readonly CryptoAvenueDbContext _dbContext;
        private readonly ICoinGeckoApiService _coinGeckoApiService;

        public GetPortfolioHistoryQueryHandler(CryptoAvenueDbContext dbContext, ICoinGeckoApiService coinGeckoApiService)
        {
            _dbContext = dbContext;
            _coinGeckoApiService = coinGeckoApiService;
        }
        public async Task<List<PortfolioHistoryDto>> Handle(GetPortfolioHistoryQuery request, CancellationToken cancellationToken)
        {
            var walletCoins = await _dbContext.WalletCoins.Where(x => x.WalletId == request.WalletId).ToListAsync();
            var portfolioHistory = new Dictionary<DateTime, decimal>();

            foreach (var walletCoin in walletCoins)
            {
                var coin = await _dbContext.Coins.FindAsync(walletCoin.CoinId);
                var coinHistory = await _coinGeckoApiService.FetchCoinHistory(coin.Id, "usd", request.Days);

                foreach (var pricePoint in coinHistory)
                {
                    if (portfolioHistory.ContainsKey(pricePoint.Key))
                    {
                        portfolioHistory[pricePoint.Key] += pricePoint.Value * (decimal)walletCoin.Quantity;
                    }
                    else
                    {
                        portfolioHistory[pricePoint.Key] = pricePoint.Value * (decimal)walletCoin.Quantity;
                    }
                }
            }

            return portfolioHistory.Select(x => new PortfolioHistoryDto
            {
                TimeStamp = x.Key,
                TotalValue = x.Value
            }).ToList();
        }
    }
    
}
