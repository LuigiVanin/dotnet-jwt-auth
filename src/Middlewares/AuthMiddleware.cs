using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserJwt.Repositories;
using UserJwt.src.Services.User;

namespace UserJwt.Middlewares
{

    public class AuthMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        private static void UnauthorizedError(HttpContext context)
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            context.Response.WriteAsJsonAsync(new { Message = "Unauthorized", StatusCode = 401 });
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration config, IUserRepository userRepository)
        {
            var authorizationString = context.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(authorizationString))
            {
                UnauthorizedError(context);
                return;
            }

            var authType = authorizationString.Split(" ")[0];
            var jwt = authorizationString.Split(" ")[1];

            if (authType != "Bearer" || string.IsNullOrEmpty(jwt))
            {
                UnauthorizedError(context);
                return;
            }

            var secretKeyString = config.GetValue<string>("JwtSecretKey");

            if (string.IsNullOrEmpty(secretKeyString))
            {
                UnauthorizedError(context);
                return;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString));

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(jwt, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = secretKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtSecurityToken = (JwtSecurityToken)validatedToken;

                string userId = jwtSecurityToken.Claims.First(claim => claim.Type == "user_id").Value ?? "";

                var user = await userRepository.FindById(userId);

                if (user == null)
                {
                    UnauthorizedError(context);
                    return;
                }


                context.Items["User"] = user;

                await _next(context);
            }
            catch
            {
                UnauthorizedError(context);
                return;
            }

        }
    }
}