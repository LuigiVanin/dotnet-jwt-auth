using System.ComponentModel.DataAnnotations;

namespace UserJwt.Dtos.Auth
{
    public class SignUpDto
    {
        [Required]
        [MinLength(3), MaxLength(120)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6), MaxLength(120)]
        public string Password { get; set; }

        [Required]
        [MinLength(6), MaxLength(120)]
        public string ConfirmPassword { get; set; }
    }
}