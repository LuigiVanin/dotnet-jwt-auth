using UserJwt.Dtos.Auth;
using UserJwt.Models;

namespace UserJwt.Services.Auth
{
    public interface IAuthService
    {
        Task<User> SignUp(SignUpDto signUpDto);
        Task<SignInResponseDto> SignIn(SignInDto signInDto);
    }
}