using CryptoAvenue.Application.Services;
using CryptoAvenue.Application.TransactionApp.TransactionCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.TransactionApp.TransactionCommandHandlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Transaction>
    {
        private readonly ICoinGeckoApiService _coinGeckoApiService;
        private readonly IWalletCoinRepository _walletCoinRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CreateTransactionCommandHandler(ICoinGeckoApiService coinGeckoApiService, IWalletCoinRepository walletCoinRepository, IWalletRepository walletRepository, ITransactionRepository transactionRepository)
        {
            _coinGeckoApiService = coinGeckoApiService;
            _walletCoinRepository = walletCoinRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var crypto = _coinGeckoApiService.GetLatestCryptoDataAsync().Result.SingleOrDefault(x => x.Id == request.CoinId);
            if (crypto != null) 
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
                    PriceChange24h = crypto.Price_Change_24h,
                    PriceChangePercentage24h = crypto.Price_Change_Percentage_24h,
                    MarketCapChange24h = crypto.Market_Cap_Change_24h,
                    MarketCapChangePercentage24h = crypto.Market_Cap_Change_Percentage_24h,
                    Ath = crypto.Ath
                };
                var price = coin.CurrentPrice;
                var transactionTotal = price * request.Quantity;

                var wallet = _walletCoinRepository.GetEntityBy(x => x.CoinId == request.CoinId && x.WalletId == request.WalletId).Result;
                var availableBalance = wallet.Quantity;
            }
            return null;
        }
        //public class CreateTransactionCommand : IRequest<Transaction>
        //{
        //    public Guid WalletId { get; set; }
        //    public string CoinId { get; set; }
        //    public string TransactionType { get; set; }
        //    public double Quantity { get; set; }
        //}
    }
}
