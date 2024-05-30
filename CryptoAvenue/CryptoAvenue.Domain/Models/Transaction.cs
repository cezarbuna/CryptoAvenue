using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class Transaction : BaseEntity
    {
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public string SourceCoinId { get; set; }
        public Coin SourceCoin { get; set; }
        public string TargetCoinId { get; set; }
        public Coin TargetCoin { get; set; }
        public string TransactionType { get; set; } // 'BUY', 'SELL' or 'TRADE'
        public DateTime TransactionDate { get; set; }
        public double SourceQuantity { get; set; }
        public double SourcePrice { get; set; } // price of source coin
        public double TargetQuantity { get; set; }
        public double TargetPrice { get; set; } // price of target coin
    }
}
