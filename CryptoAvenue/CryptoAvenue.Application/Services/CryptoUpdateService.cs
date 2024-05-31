using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Services
{
    public class CryptoUpdateService : ICryptoUpdateService
    {
        private readonly CryptoAvenueDbContext _dbContext;
        private readonly ICoinGeckoApiService _coinGeckoApiService;
        private readonly ILogger<CryptoUpdateService> _logger;

        public CryptoUpdateService(CryptoAvenueDbContext dbContext, ICoinGeckoApiService coinGeckoApiService, ILogger<CryptoUpdateService> logger)
        {
            _dbContext = dbContext;
            _coinGeckoApiService = coinGeckoApiService;
            _logger = logger;
        }

        public async Task UpdateCryptoCurrenciesAsync()
        {
            var cryptos = await _coinGeckoApiService.GetLatestCryptoDataAsync();
            var coins = new List<Coin>();
            //to create mapping later..
            foreach (var crypto in cryptos)
            {
                var coin = new Coin
                {
                    Id = crypto.Id,
                    Symbol = crypto.Symbol,
                    Name = crypto.Name,
                    ImageUrl = crypto.Image,
                    CurrentPrice = crypto.Current_Price,
                    MarketCap = crypto.Market_Cap,
                    MarketCapRank = crypto.Market_Cap_Rank,
                    High24h = crypto.High_24h,
                    Low24h = crypto.Low_24h,
                    PriceChange24h = crypto.Price_Change24h,
                    PriceChangePercentage24h = crypto.Price_Change_Percentage24h,
                    MarketCapChange24h = crypto.Market_Cap_Change_24h,
                    MarketCapChangePercentage24h = crypto.Market_Cap_Change_Percentage24h,
                    Ath = crypto.Ath
                };
                coins.Add(coin);
            }
            foreach (var coin in coins)
            {
                _dbContext.Coins.Update(coin);
                await _dbContext.SaveChangesAsync();

                //also update user's portfolios

            }
            _logger.LogInformation("All Database updated successfully.");
        }
    }
}
