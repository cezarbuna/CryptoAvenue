using CryptoAvenue.Application.CoinDtos;
using CryptoAvenue.Application.WalletApp.WalletQueries;
using CryptoAvenue.Domain.IRepositories;
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
    public class GetPortfolioInformationQueryHandler : IRequestHandler<GetPortfolioInformationQuery, List<CoinValueDto>>
    {
        private CryptoAvenueDbContext _dbContext;

        public GetPortfolioInformationQueryHandler(CryptoAvenueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CoinValueDto>> Handle(GetPortfolioInformationQuery request, CancellationToken cancellationToken)
        {
            var result = new List<CoinValueDto>();
            var walletsCoins = await _dbContext.WalletCoins.Where(x => x.WalletId == request.WalletId).ToListAsync();
            if(walletsCoins != null)
            {
                foreach (var walletCoin in walletsCoins)
                {
                    var coin = await _dbContext.Coins.SingleOrDefaultAsync(x => x.Id == walletCoin.CoinId);
                    if(coin != null)
                    {
                        var newCoinValueDto = new CoinValueDto
                        {
                            Id = coin.Id,
                            CurrentPrice = coin.CurrentPrice,
                            MarketCap = coin.MarketCap,
                            Name = coin.Name,
                            PriceChangePercentage24h = coin.PriceChangePercentage24h,
                            Symbol = coin.Symbol,
                            ImageUrl = coin.ImageUrl,
                            Amount = walletCoin.Quantity
                        };
                        result.Add(newCoinValueDto);
                    }
                }
            }
            return result;
        }
    }
}
