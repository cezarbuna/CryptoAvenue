using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class Coin : Entity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public double ValueInEur { get; set; }
        public double ValueInUsd { get; set; }
    }
}
