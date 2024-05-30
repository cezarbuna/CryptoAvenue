using CryptoAvenue.Application.WalletApp.WalletCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletCommandHandlers
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Wallet>
    {
        private readonly IWalletRepository _walletRepository;

        public CreateWalletCommandHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<Wallet> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            if(_walletRepository.Any(x => x.UserId == request.UserId))
            {
                return await _walletRepository.GetEntityBy(x => x.UserId == request.UserId);
            }
            
            var wallet = new Wallet
            {
                UserId = request.UserId,
                Balance = 0
            };
            await _walletRepository.Insert(wallet);
            await _walletRepository.SaveChanges();
           return wallet;
        }
    }
}
