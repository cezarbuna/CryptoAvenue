using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class Coin 
    {
        public string Id { get; set; } 
        public string Symbol { get; set; } 
        public string Name { get; set; } 
        public double CurrentPrice { get; set; } 
        public double MarketCap { get; set; } 
        public int MarketCapRank { get; set; } 
        public double High24h { get; set; }
        public double Low24h { get; set; }
        public double PriceChange24h { get; set; }
        public double PriceChangePercentage24h { get; set; }
        public double MarketCapChange24h { get; set; }
        public double MarketCapChangePercentage24h { get; set; }
        public double Ath { get; set; }
        public string ImageUrl { get; set; }

    }
}
