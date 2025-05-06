using System;
using System.ComponentModel.DataAnnotations;
using Pertiv_be_with_dotnet.Enums;

namespace Pertiv_be_with_dotnet.Dtos
{
    public class UserDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public bool Is_Deleted { get; set; } = false;
    }

    
}
