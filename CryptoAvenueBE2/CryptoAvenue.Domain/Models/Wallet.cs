using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class Wallet : Entity
    {
        public Guid CoinId { get; set; }
        public Coin Coin { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public double CoinAmount { get; set; }
    }
}
