using CryptoAvenue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.IRepositories
{
    public interface IWalletRepository : IGenericRepository<Wallet>
    {
        Task<Wallet> GetByUserId(Guid userId);
    }
}
