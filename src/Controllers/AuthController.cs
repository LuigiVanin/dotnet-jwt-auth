using Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserJwt.Helpers;
using UserJwt.Dtos.Auth;
using UserJwt.Services.Auth;

namespace UserJwt.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("signup", Name = "SignUp")]
        [SwaggerOperation(
            Summary = "Signup route to create new user"
        )]
        public async Task<ActionResult<UserDto>> SignUp([FromBody] SignUpDto signData)
        {
            try
            {
                return Ok(await _authService.SignUp(signData));
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.ToJson());
            }
        }

        [HttpPost("signin", Name = "SignIn")]
        [SwaggerOperation(
            Summary = "Signin route to authenticate user. It should retrieve the user and the jwt"
        )]
        public async Task<ActionResult<SignInResponseDto>> SignIn([FromBody] SignInDto signData)
        {
            try
            {
                return Ok(await _authService.SignIn(signData));
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.ToJson());
            }
        }

    }
}