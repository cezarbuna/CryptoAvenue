using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class TradeOffer : Entity
    {
        public User Sender { get; set; }
        public User Recipient { get; set; }
        public Coin SentCoin { get; set; }
        public Coin ReceivedCoin { get; set; }
        public double SentAmount { get; set; }
        public double ReceivedAmount { get; set; }
    }
}
