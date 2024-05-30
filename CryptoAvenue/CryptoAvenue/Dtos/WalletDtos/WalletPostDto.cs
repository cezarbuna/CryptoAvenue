using System.ComponentModel.DataAnnotations;

namespace CryptoAvenue.Dtos.WalletDtos
{
    public class WalletPostDto
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
