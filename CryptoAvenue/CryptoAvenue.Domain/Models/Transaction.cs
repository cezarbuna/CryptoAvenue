using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class Transaction : BaseEntity
    {
        public Wallet Wallet { get; set; }
        public Guid WalletId { get; set; }
        public Coin Coin { get; set; }
        public string CoinId { get; set; }
        public string TransactionType { get; set; } // 'BUY', 'SELL' or 'TRADE'
        public DateTime TransactionDate { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; } // price of coin
        public double Total { get; set; } // quantity * price
    }
}
