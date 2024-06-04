using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.TransactionApp.TransactionQueries
{
    public class GetTransactionsByUserIdQuery : IRequest<List<Transaction>>
    {
        public Guid UserId { get; set; }
    }
}
