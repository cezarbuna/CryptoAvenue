using CryptoAvenue.Dtos.CoinDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Services
{
    public interface ICoinGeckoApiService
    {
        Task<IEnumerable<CoinGetDto>> GetLatestCryptoDataAsync();
    }
}
