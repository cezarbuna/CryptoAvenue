using CryptoAvenue.Application.WalletApp.WalletQueries;
using CryptoAvenue.Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletQueryHandlers
{
    public class GetUserPotrfolioQueryHandler : IRequestHandler<GetUserPotrfolioQuery, string>
    {
        private readonly CryptoAvenueDbContext _dbContext;

        public GetUserPotrfolioQueryHandler(CryptoAvenueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetUserPotrfolioQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _dbContext.Wallets.SingleOrDefaultAsync(x => x.UserId == request.UserId);
            return "Hello";
        }
    }
}
