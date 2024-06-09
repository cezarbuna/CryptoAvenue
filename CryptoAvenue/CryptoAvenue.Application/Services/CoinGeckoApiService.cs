using CryptoAvenue.Application.CoinDtos;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.CoinDtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Services
{
    public class CoinGeckoApiService : ICoinGeckoApiService
    {
        private readonly HttpClient _httpClient;
        private readonly CryptoAvenueDbContext _dbContext;

        public CoinGeckoApiService(HttpClient httpClient, CryptoAvenueDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CoinGetDto>> GetLatestCryptoDataAsync()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoAvenue");

            var requestUrl = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd";

            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cryptoData = JsonConvert.DeserializeObject<IEnumerable<CoinGetDto>>(content);
                return cryptoData;
            }
            else
            {
                throw new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
            }
        }
        public async Task<List<Coin>> FetchUserCoins(Guid walletId)
        {
            var walletCoins =  await _dbContext.WalletCoins.Where(x => x.WalletId == walletId).ToListAsync();
            var result = new List<Coin>();
            foreach (var item in walletCoins)
            {
                result.Add(_dbContext.Coins.SingleOrDefaultAsync(x => x.Id == item.CoinId).Result);
            }
            return result;
        }
        public async Task<Dictionary<DateTime, decimal>> FetchCoinHistory(string coinId, string currency, int days)
        {
            var url = $"https://api.coingecko.com/api/v3/coins/{coinId}/market_chart?vs_currency={currency}&days={days}";
            var response = await _httpClient.GetStringAsync(url);
            var data = JObject.Parse(response);

            var prices = new Dictionary<DateTime, decimal>();
            foreach (var item in data["prices"])
            {
                long timestamp = item[0].ToObject<long>();
                decimal price = item[1].ToObject<decimal>();
                var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;
                prices[dateTime] = price;
            }
            return prices;
        }

    }
}
