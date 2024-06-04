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
    public class RevertTransactionCommandHandler : IRequestHandler<RevertTransactionCommand, Transaction>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletCoinRepository _walletCoinRepository;
        private readonly ICoinGeckoApiService _coinGeckoApiService;
        private readonly ITransactionRepository _transactionRepository;

        public RevertTransactionCommandHandler(IWalletRepository walletRepository, IWalletCoinRepository walletCoinRepository, ICoinGeckoApiService coinGeckoApiService, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _walletCoinRepository = walletCoinRepository;
            _coinGeckoApiService = coinGeckoApiService;
            _transactionRepository = transactionRepository;
        }

        public Task<Transaction> Handle(RevertTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
