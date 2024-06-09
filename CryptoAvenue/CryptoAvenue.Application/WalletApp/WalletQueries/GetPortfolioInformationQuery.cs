using CryptoAvenue.Application.CoinDtos;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.CoinDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.WalletApp.WalletQueries
{
    public class GetPortfolioInformationQuery : IRequest<List<CoinValueDto>>
    {
        public Guid WalletId { get; set; }
    }
}
