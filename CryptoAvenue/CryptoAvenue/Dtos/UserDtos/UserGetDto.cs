using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Dal.Dtos.UserDtos
{
    public class UserGetDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int Age { get; set; } 
    }
}
