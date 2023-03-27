using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
    }
}
