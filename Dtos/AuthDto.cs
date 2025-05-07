using System.ComponentModel.DataAnnotations;

namespace Pertiv_be_with_dotnet.Dtos
{
    public class AuthDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
