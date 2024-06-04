using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Transactions;

namespace CryptoAvenue.Application.TransactionApp.TransactionCommands
{
    public class RevertTransactionCommand : IRequest<Transaction>
    {
        public Guid TransactionId { get; set; }
    }
}
