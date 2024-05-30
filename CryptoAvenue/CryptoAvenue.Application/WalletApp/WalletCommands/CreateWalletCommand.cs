using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletCommands
{
    public class CreateWalletCommand : IRequest<Wallet>
    {
        public Guid UserId { get; set; }
    }
}
