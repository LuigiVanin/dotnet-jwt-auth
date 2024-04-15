using System.ComponentModel.DataAnnotations;
using UserJwt.Dtos.User;

namespace UserJwt.Dtos.Auth
{
    public class SignInDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6), MaxLength(120)]
        public string Password { get; set; }
    }

    public class SignInResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}


