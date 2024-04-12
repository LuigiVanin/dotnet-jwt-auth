using Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace UserJwt.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/user")]
    public class UserController() : ControllerBase
    {
        [HttpGet("{id}", Name = "GetUser")]
        [SwaggerOperation(
            Summary = "Fetch specific user data. It only works if the user is authenticated and try to fetch it self."
        )]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] string id)
        {
            Console.WriteLine(id);

            return Ok(new UserDto
            {
                Name = "John Doe",
                Email = ""
            });
        }
    }

}