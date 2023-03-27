using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class CreditCard : Entity
    {
        public User User { get; set; }
        public string CardNumber { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public int Cvv { get; set; }
    }
}
