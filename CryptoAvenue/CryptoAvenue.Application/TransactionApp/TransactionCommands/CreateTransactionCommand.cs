using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.TransactionApp.TransactionCommands
{
    public class CreateTransactionCommand : IRequest<Transaction>
    {
        public Guid WalletId { get; set; }
        public string CoinId { get; set; }
        public string TransactionType { get; set; }
        public double Quantity { get; set; }
    }
}
