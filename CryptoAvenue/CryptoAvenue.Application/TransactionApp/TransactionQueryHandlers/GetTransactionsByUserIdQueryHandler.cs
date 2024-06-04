using CryptoAvenue.Application.TransactionApp.TransactionQueries;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
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

        public GetTransactionsByUserIdQueryHandler(ITransactionRepository transactionRepository, IWalletRepository walletRepository)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        public async Task<List<Transaction>> Handle(GetTransactionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetEntityBy(x => x.UserId == request.UserId);
            return _transactionRepository.FindAll(x => x.WalletId == wallet.Id).Result.ToList();
        }
    }
}
