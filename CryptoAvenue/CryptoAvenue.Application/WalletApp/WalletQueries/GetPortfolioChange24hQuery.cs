using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletQueries
{
    public class GetPortfolioChange24hQuery : IRequest<double>
    {
        public Guid WalletId { get; set; }
    }
}
