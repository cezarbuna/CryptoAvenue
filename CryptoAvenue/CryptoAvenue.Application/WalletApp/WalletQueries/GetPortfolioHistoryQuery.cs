using CryptoAvenue.Application.CoinDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletQueries
{
    public class GetPortfolioHistoryQuery : IRequest<List<PortfolioHistoryDto>>
    {
        public Guid WalletId { get; set; }
        public int Days { get; set; }
    }
}
