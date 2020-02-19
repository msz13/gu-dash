using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompetencesService.Infrastructure.Auth
{
    public class AuthenticationMiddlewere
    {
        RequestDelegate _next;

        public AuthenticationMiddlewere(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userId = context.Request.Headers["X-Authenticated-UserId"];

            if (String.IsNullOrEmpty(userId)) throw new AuthenticationException("Request with nul or empty 'X - Authenticated - UserId'");

            var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
            context.User.AddIdentity(identity);
            await _next(context);

            
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddlewere(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddlewere>();
        }
    }
}
