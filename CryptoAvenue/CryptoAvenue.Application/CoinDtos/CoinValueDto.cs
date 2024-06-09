using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.CoinDtos
{
    public class CoinValueDto
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double CurrentPrice { get; set; }
        public double MarketCap { get; set; }
        public double PriceChangePercentage24h { get; set; }
        public string ImageUrl { get; set; }
        public double Amount { get; set; }
    }
}
