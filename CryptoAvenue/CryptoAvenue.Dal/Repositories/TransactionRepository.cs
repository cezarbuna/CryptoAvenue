using CryptoAvenue.Application;
using CryptoAvenue.Application.Repositories;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Dal.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(CryptoAvenueDbContext context) : base(context)
        {
        }
    }
}
