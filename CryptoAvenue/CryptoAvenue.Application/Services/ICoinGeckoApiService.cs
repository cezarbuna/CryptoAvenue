using CryptoAvenue.Domain.Models;
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
        Task<Dictionary<DateTime, decimal>> FetchCoinHistory(string coinId, string currency, int days);
        Task<List<Coin>> FetchUserCoins(Guid walletId);
    }
}
