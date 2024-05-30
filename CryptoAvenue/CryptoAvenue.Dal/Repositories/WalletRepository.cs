using CryptoAvenue.Application;
using CryptoAvenue.Application.Repositories;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Dal.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {

        public WalletRepository(CryptoAvenueDbContext context) : base(context)
        {
        }

        public Task<Wallet> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<Wallet> GetByUserId(Guid userId)
        //{
        //    return await this.dbSet.SingleOrDefaultAsync(x => x.UserId == userId);
        //}
    }
}
