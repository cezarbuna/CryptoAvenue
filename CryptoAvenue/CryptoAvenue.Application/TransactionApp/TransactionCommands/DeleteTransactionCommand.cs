using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.TransactionApp.TransactionCommands
{
    public class DeleteTransactionCommand : IRequest<Transaction>
    {
        public Guid TransactionId { get; set; }
    }
}
