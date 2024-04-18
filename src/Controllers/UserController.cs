using UserJwt.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserJwt.Models;
using UserJwt.src.Services.User;

namespace UserJwt.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/user")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("{id}", Name = "GetUser")]
        [SwaggerOperation(
            Summary = "Fetch specific user data. It only works if the user is authenticated and try to fetch it self."
        )]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] string id)
        {
            User? user = HttpContext.Items["User"] as User;

            if (user == null || user.Id != id)
            {
                return Unauthorized();
            }

            return Ok(await _userService.FindUserById(id));
        }
    }

}