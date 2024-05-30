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
    public class GetWalletByIdQueryHandler : IRequestHandler<GetWalletByIdQuery, Wallet>
    {
        private readonly IWalletRepository _walletRepository;

        public GetWalletByIdQueryHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<Wallet> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
        {
            return await _walletRepository.GetEntityByID(request.WalletId);
        }
    }
}
