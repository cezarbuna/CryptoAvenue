using CryptoAvenue.Application.Services;
using CryptoAvenue.Application.WalletApp.WalletCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.CoinDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletCommandHandlers
{
    public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, Wallet>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletCoinRepository _walletCoinRepository;
        private readonly ICoinGeckoApiService _coinGeckoApiService;
        private readonly ITransactionRepository _transactionRepository;

        public WithdrawCommandHandler(IWalletRepository walletRepository, IWalletCoinRepository walletCoinRepository, ICoinGeckoApiService coinGeckoApiService, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _walletCoinRepository = walletCoinRepository;
            _coinGeckoApiService = coinGeckoApiService;
            _transactionRepository = transactionRepository;
        }

        public async Task<Wallet> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var usdCoin = _coinGeckoApiService.GetLatestCryptoDataAsync().Result.SingleOrDefault(x => x.Id == "usd-coin");

                var foundWallet = await _walletRepository.GetEntityBy(x => x.UserId == request.UserId);
                foundWallet.Balance -= request.Quantity;
                await _walletRepository.Update(foundWallet);
                await _walletRepository.SaveChanges();

                var usdWallet = await _walletCoinRepository.GetEntityBy(x => x.WalletId == foundWallet.Id && x.CoinId == usdCoin.Id);
                usdWallet.Quantity -= request.Quantity;

                if(usdWallet.Quantity <= 0)
                {
                    _walletCoinRepository.Delete(usdWallet);
                }
                else
                {
                    await _walletCoinRepository.Update(usdWallet);
                }
                await _walletCoinRepository.SaveChanges();

                var transaction = await CreateTransaction(foundWallet, usdCoin, request.Quantity);

                return foundWallet;
        }
        public async Task<Transaction> CreateTransaction(Wallet wallet, CoinGetDto coin, double quantity)
        {
            var transaction = new Transaction
            {
                SourceCoinId = coin.Id,
                TargetCoinId = coin.Id,
                SourcePrice = coin.Current_Price,
                TargetPrice = coin.Current_Price,
                SourceQuantity = quantity,
                TargetQuantity = quantity,
                TransactionType = "SELL",
                TransactionDate = DateTime.Now,
                WalletId = wallet.Id,
            };
            await _transactionRepository.Insert(transaction);
            await _transactionRepository.SaveChanges();

            return transaction;
        }
    }
}
