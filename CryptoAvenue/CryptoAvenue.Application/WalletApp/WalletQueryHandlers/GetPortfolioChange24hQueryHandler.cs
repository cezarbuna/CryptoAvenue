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
    public class GetPortfolioChange24hQueryHandler : IRequestHandler<GetPortfolioChange24hQuery, double>
    {
        private readonly CryptoAvenueDbContext _dbContext;

        public GetPortfolioChange24hQueryHandler(CryptoAvenueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<double> Handle(GetPortfolioChange24hQuery request, CancellationToken cancellationToken)
        {
            double balance24hAgo = 0;
            var wallet = await _dbContext.Wallets.SingleOrDefaultAsync(x => x.Id == request.WalletId);
            if(wallet != null)
            {
                var walletsCoins = await _dbContext.WalletCoins.Where(x => x.WalletId == request.WalletId).ToListAsync();

                if(walletsCoins.Count > 0)
                {
                    foreach (var walletCoin in walletsCoins)
                    {
                        var coin = _dbContext.Coins.SingleOrDefault(x => x.Id == walletCoin.CoinId);
                        var price24hAgo = coin.CurrentPrice * ((100.00 - coin.PriceChange24h) / 100);
                        balance24hAgo += price24hAgo * walletCoin.Quantity;
                    }
                }
                return 100.00 * ((wallet.Balance /  balance24hAgo) / balance24hAgo );
            }
            return balance24hAgo;
        }
    }
}
