using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserJwt.Helpers;

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

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("\n================== Middleware ==================\n");
            var authorizationString = context.Request.Headers["Authorization"].ToString();
            Console.WriteLine($"token {authorizationString}");

            if (string.IsNullOrEmpty(authorizationString))
            {
                UnauthorizedError(context);
                return;
            }

            var authType = authorizationString.Split(" ")[0];
            var jwt = authorizationString.Split(" ")[1];
            Console.WriteLine($"JWT TOKEN: {jwt}");

            if (authType != "Bearer" || string.IsNullOrEmpty(jwt))
            {
                UnauthorizedError(context);
                return;
            }



            await _next(context);
        }
    }
}