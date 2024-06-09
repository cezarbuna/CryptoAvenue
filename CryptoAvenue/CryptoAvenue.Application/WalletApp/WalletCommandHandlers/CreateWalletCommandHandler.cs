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
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Wallet>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletCoinRepository _walletCoinRepository;
        private readonly ICoinGeckoApiService _coinGeckoApiService;
        private readonly ITransactionRepository _transactionRepository;

        public CreateWalletCommandHandler(IWalletRepository walletRepository, IWalletCoinRepository walletCoinRepository, ICoinGeckoApiService coinGeckoApiService, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _walletCoinRepository = walletCoinRepository;
            _coinGeckoApiService = coinGeckoApiService;
            _transactionRepository = transactionRepository;
        }

        public async Task<Wallet> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var usdCoin = _coinGeckoApiService.GetLatestCryptoDataAsync().Result.SingleOrDefault(x => x.Id == "usd-coin");

            if (_walletRepository.Any(x => x.UserId == request.UserId)) //if the user already has a wallet
            {
                var foundWallet = await _walletRepository.GetEntityBy(x => x.UserId == request.UserId);
                foundWallet.Balance += request.Quantity;
                await _walletRepository.Update(foundWallet);
                await _walletRepository.SaveChanges();

                var usdWallet = await _walletCoinRepository.GetEntityBy(x => x.WalletId == foundWallet.Id && x.CoinId == usdCoin.Id);
                usdWallet.Quantity += request.Quantity;
                await _walletCoinRepository.Update(usdWallet);
                await _walletCoinRepository.SaveChanges();

                var transaction = await CreateTransaction(foundWallet, usdCoin, request.Quantity);

                return foundWallet;
            }

            //else..
            
            var wallet = new Wallet
            {
                UserId = request.UserId,
                Balance = request.Quantity
            };
            await _walletRepository.Insert(wallet);
            await _walletRepository.SaveChanges();

            var walletCoin = await _walletCoinRepository.GetEntityBy(x => x.WalletId == wallet.Id && x.CoinId == usdCoin.Id);

            if(walletCoin == null)
            {
                var newUsdWallet = new WalletCoin
                {
                    WalletId = wallet.Id,
                    CoinId = usdCoin.Id,
                    Quantity = request.Quantity
                };
                await _walletCoinRepository.Insert(newUsdWallet);
                await _walletCoinRepository.SaveChanges();
            }
            else
            {
                walletCoin.Quantity += request.Quantity;
                await _walletCoinRepository.Update(walletCoin);
                await _walletCoinRepository.SaveChanges();
            }
            var newTransaction = await CreateTransaction(wallet, usdCoin, request.Quantity);
            return wallet;
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
                TransactionType = "BUY",
                TransactionDate = DateTime.Now,
                WalletId = wallet.Id,
            };

            await _transactionRepository.Insert(transaction);
            await _transactionRepository.SaveChanges();
            
            return transaction;
        }
    }
}
