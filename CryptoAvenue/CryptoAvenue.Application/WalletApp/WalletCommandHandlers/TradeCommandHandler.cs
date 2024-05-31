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
    public class TradeCommandHandler : IRequestHandler<TradeCommand, Wallet>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletCoinRepository _walletCoinRepository;
        private readonly ICoinGeckoApiService _coinGeckoApiService;
        private readonly ITransactionRepository _transactionRepository;

        public TradeCommandHandler(IWalletRepository walletRepository, IWalletCoinRepository walletCoinRepository, ICoinGeckoApiService coinGeckoApiService, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _walletCoinRepository = walletCoinRepository;
            _coinGeckoApiService = coinGeckoApiService;
            _transactionRepository = transactionRepository;
        }

        public async Task<Wallet> Handle(TradeCommand request, CancellationToken cancellationToken)
        {
            var sourceCoin = _coinGeckoApiService.GetLatestCryptoDataAsync().Result.SingleOrDefault(x => x.Id == request.SourceCoinId);
            var targetCoin = _coinGeckoApiService.GetLatestCryptoDataAsync().Result.SingleOrDefault(x => x.Id == request.TargetCoinId);

            var wallet = await _walletRepository.GetEntityBy(x => x.UserId == request.UserId);
            var sourceCoinWallet = await _walletCoinRepository.GetEntityBy(x => x.CoinId == request.SourceCoinId && x.WalletId == wallet.Id);

            sourceCoinWallet.Quantity -= request.SourceQuantity;
            if(sourceCoinWallet.Quantity <= 0)
            { 
                _walletCoinRepository.Delete(sourceCoinWallet);
            }
            else
            {
                await _walletCoinRepository.Update(sourceCoinWallet);
            }
            await _walletCoinRepository.SaveChanges();

            var targetCoinWallet = await _walletCoinRepository.GetEntityBy(x => x.CoinId == request.TargetCoinId && x.WalletId == wallet.Id);

            if(targetCoinWallet == null) // if user has a wallet of the target coin
            {
                var newTargetCoinWallet = new WalletCoin
                {
                    CoinId = request.TargetCoinId,
                    WalletId = wallet.Id,
                    Quantity = request.TargetQuantity
                };
                await _walletCoinRepository.Insert(newTargetCoinWallet);
            }
            else // if user already has wallet of this type
            {
                targetCoinWallet.Quantity += request.TargetQuantity;
                await _walletCoinRepository.Update(targetCoinWallet);
            }
            var newTransaction = await CreateTransaction(wallet, sourceCoin, targetCoin, request.SourceQuantity, request.TargetQuantity);

            await _walletCoinRepository.SaveChanges();
            return wallet;
        }
        public async Task<Transaction> CreateTransaction(Wallet wallet, CoinGetDto sourceCoin,CoinGetDto targetCoin, double sourceQuantity, double targetQuantity)
        {
            var transaction = new Transaction
            {
                SourceCoinId = sourceCoin.Id,
                TargetCoinId = targetCoin.Id,
                SourcePrice = sourceCoin.Current_Price,
                TargetPrice = targetCoin.Current_Price,
                SourceQuantity = sourceQuantity,
                TargetQuantity = targetQuantity,
                TransactionType = "TRADE",
                TransactionDate = DateTime.Now,
                WalletId = wallet.Id,
            };
            await _transactionRepository.Insert(transaction);
            await _transactionRepository.SaveChanges();

            return transaction;
        }
    }
}
