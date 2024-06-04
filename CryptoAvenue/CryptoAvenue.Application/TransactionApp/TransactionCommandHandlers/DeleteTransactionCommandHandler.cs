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
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Transaction>
    {
        private readonly ITransactionRepository transactionRepository;

        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public async Task<Transaction> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await transactionRepository.GetEntityByID(request.TransactionId);
            transactionRepository.Delete(transaction);
            await transactionRepository.SaveChanges();
            return transaction;
        }
    }
}
