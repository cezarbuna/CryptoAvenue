using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class Wallet : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public double Balance { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); // navigation property
    }
}
