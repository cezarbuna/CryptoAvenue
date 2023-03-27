using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class Admin : Entity
    {
        public string AdminUsername  { get; set; }
        public string AdminPassword { get; set; }
    }
}
