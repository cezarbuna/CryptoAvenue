using CryptoAvenue.Application.WalletApp.WalletQueries;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletQueryHandlers
{
    public class GetWalletByUserIdQueryHandler : IRequestHandler<GetWalletByUserIdQuery, Wallet>
    {
        private readonly IWalletRepository walletRepository;

        public GetWalletByUserIdQueryHandler(IWalletRepository walletRepository)
        {
            this.walletRepository = walletRepository;
        }

        public async Task<Wallet> Handle(GetWalletByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await walletRepository.GetEntityBy(x => x.UserId == request.UserId);
        }
    }
}
