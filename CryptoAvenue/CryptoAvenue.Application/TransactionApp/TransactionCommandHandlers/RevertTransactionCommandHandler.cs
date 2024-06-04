using CryptoAvenue.Application.Services;
using CryptoAvenue.Application.TransactionApp.TransactionCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.TransactionApp.TransactionCommandHandlers
{
    public class RevertTransactionCommandHandler : IRequestHandler<RevertTransactionCommand, Transaction>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletCoinRepository _walletCoinRepository;
        private readonly CryptoAvenueDbContext _dbContext;
        private readonly ITransactionRepository _transactionRepository;

        public RevertTransactionCommandHandler(IWalletRepository walletRepository, IWalletCoinRepository walletCoinRepository, CryptoAvenueDbContext dbContext, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _walletCoinRepository = walletCoinRepository;
            _dbContext = dbContext;
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> Handle(RevertTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetEntityByID(request.TransactionId);
            var sourceCoin = await _dbContext.Coins.SingleOrDefaultAsync(x => x.Id == transaction.SourceCoinId);
            var targetCoin = await _dbContext.Coins.SingleOrDefaultAsync(x => x.Id == transaction.TargetCoinId);

            var wallet = await _walletRepository.GetEntityByID(transaction.WalletId);
            var targetCoinWallet = await _walletCoinRepository.GetEntityBy(x => x.WalletId == wallet.Id && x.CoinId == targetCoin.Id);
            if(targetCoinWallet.Quantity < request.TargetAmount || targetCoinWallet == null)
            {
                return null;
            }
            targetCoinWallet.Quantity -= transaction.TargetQuantity;
            if (targetCoinWallet.Quantity <= 0)
                _walletCoinRepository.Delete(targetCoinWallet);
            else
                await _walletCoinRepository.Update(targetCoinWallet);
            await _walletCoinRepository.SaveChanges();

            var sourceCoinWallet = await _walletCoinRepository.GetEntityBy(x => x.WalletId == wallet.Id && x.CoinId == sourceCoin.Id);
            if(sourceCoinWallet == null)
            {
                var newSourceCoinWallet = new WalletCoin
                {
                    CoinId = sourceCoin.Id,
                    WalletId = wallet.Id,
                    Quantity = transaction.SourceQuantity
                };
                await _walletCoinRepository.Insert(newSourceCoinWallet);
            }
            else
            {
                sourceCoinWallet.Quantity += transaction.SourceQuantity;
                await _walletCoinRepository.Update(sourceCoinWallet);
                
            }
            await _walletCoinRepository.SaveChanges();
            var newTransaction = await CreateTransaction(wallet, targetCoin, sourceCoin, transaction.TargetQuantity, transaction.SourceQuantity);
            _transactionRepository.Delete(transaction);
            await _transactionRepository.SaveChanges();
            return newTransaction;
        }
        public async Task<Transaction> CreateTransaction(Wallet wallet, Coin sourceCoin, Coin targetCoin, double sourceQuantity, double targetQuantity)
        {
            var transaction = new Transaction
            {
                SourceCoinId = sourceCoin.Id,
                TargetCoinId = targetCoin.Id,
                SourcePrice = sourceCoin.CurrentPrice,
                TargetPrice = targetCoin.CurrentPrice,
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
