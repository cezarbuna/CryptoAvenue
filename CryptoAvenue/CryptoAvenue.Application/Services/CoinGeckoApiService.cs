using CryptoAvenue.Dtos.CoinDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Services
{
    public class CoinGeckoApiService : ICoinGeckoApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CoinGeckoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "https://api.coingecko.com/api/v3/";
        }

        public async Task<IEnumerable<CoinGetDto>> GetLatestCryptoDataAsync()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoAvenue");

            var requestUrl = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=eur";

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
    }
}
