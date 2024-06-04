using CryptoAvenue.Application.Services;
using CryptoAvenue.Application.TransactionApp.TransactionQueries;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.TransactionApp.TransactionQueryHandlers
{
    public class GetTransactionsByUserIdQueryHandler : IRequestHandler<GetTransactionsByUserIdQuery, List<Transaction>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;
        private CryptoAvenueDbContext _context;

        public GetTransactionsByUserIdQueryHandler(ITransactionRepository transactionRepository, IWalletRepository walletRepository, CryptoAvenueDbContext context)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _context = context;
        }

        public async Task<List<Transaction>> Handle(GetTransactionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetEntityBy(x => x.UserId == request.UserId);
            var transactions = _transactionRepository.FindAll(x => x.WalletId == wallet.Id).Result.ToList();
            var coins = await _context.Coins.ToListAsync(cancellationToken);
            foreach (var transaction in transactions)
            {
                var sourceCoin = coins.SingleOrDefault(x => x.Id == transaction.SourceCoinId);
                var targetCoin = coins.SingleOrDefault(x => x.Id == transaction.TargetCoinId);
                transaction.SourceCoin = sourceCoin;
                transaction.TargetCoin = targetCoin;
                
            }
            return transactions;
        }
    }
}
