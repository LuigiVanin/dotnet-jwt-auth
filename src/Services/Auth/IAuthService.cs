using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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