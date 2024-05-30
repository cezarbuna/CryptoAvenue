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
    }
}
