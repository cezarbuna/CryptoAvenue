using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Dal.Dtos.UserDtos
{
    public class UserPostDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(10)]
        public string Password { get; set; }
        [Required]
        [Range(18, int.MaxValue, ErrorMessage = "You must be at least 18 years old.")]
        public int Age { get; set; } // must be over 18
    }
}
