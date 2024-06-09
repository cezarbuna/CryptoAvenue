using CryptoAvenue.Application.WalletApp.WalletQueries;
using CryptoAvenue.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletQueryHandlers
{
    public class GetUserPotrfolioQueryHandler : IRequestHandler<GetUserPotrfolioQuery, string>
    {
        private readonly CryptoAvenueDbContext _dbContext;

        public GetUserPotrfolioQueryHandler(CryptoAvenueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetUserPotrfolioQuery request, CancellationToken cancellationToken)
        {
            var result = "";
            result += "All CryptoCurrencies on the market:\n";

            var coins = await _dbContext.Coins.ToListAsync();
            foreach (var coin in coins)
            {
                result += "Id: " + coin.Name + "\n";
                result += "Symbol: " + coin.Symbol + "\n";
                result += "Name: " + coin.Name + "\n";
                result += "Current price: " + coin.CurrentPrice + "\n";
                result += "Market Cap: " + coin.MarketCap + "\n";
                result += "High 24h: " + coin.High24h + "\n";
                result += "Low 24h: " + coin.Low24h + "\n";
                result += "Price change 24h: " + coin.PriceChange24h + "\n";
                result += "Price change percentage 24h: " + coin.PriceChangePercentage24h + "\n";
                result += "Market cap change 24h: " + coin.MarketCapChange24h + "\n";
                result += "Market cap change percentage 24h: " + coin.MarketCapChangePercentage24h + "\n";
                result += "All time high: " + coin.Ath + "\n";
            }

            var wallet = await _dbContext.Wallets.SingleOrDefaultAsync(x => x.UserId == request.UserId);

            result += "Total avaialble balance user can use for trading (in usd): " + wallet.Balance + "\n";

            var walletsCoins = await _dbContext.WalletCoins.Where(x => x.WalletId == wallet.Id).ToListAsync();

            result += "All CryptoCurrencies the user posseses: \n";

            foreach (var walletCoin in walletsCoins)
            {
                result += "Coin: " + walletCoin.CoinId + " amount: " + walletCoin.Quantity + "\n";
            }
            return result;
        }
    }
}
