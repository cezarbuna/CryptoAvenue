using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletCommands
{
    public class TradeCommand : IRequest<Wallet>
    {
        public Guid UserId { get; set; }
        public string SourceCoinId { get; set; }
        public double SourceQuantity { get; set; }
        public string TargetCoinId { get; set; }
        public double TargetQuantity { get; set; }
    }
}
