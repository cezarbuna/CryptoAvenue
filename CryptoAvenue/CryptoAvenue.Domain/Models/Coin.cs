using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class Coin 
    {
        public string Id { get; set; } // Unique identifier for the cryptocurrency
        public string Symbol { get; set; } // Ticker symbol of the cryptocurrency
        public string Name { get; set; } // Name of the cryptocurrency
        public double CurrentPrice { get; set; } // Current price of the cryptocurrency
        public double MarketCap { get; set; } // Market capitalization
        public int MarketCapRank { get; set; } // Rank # on the market 
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
