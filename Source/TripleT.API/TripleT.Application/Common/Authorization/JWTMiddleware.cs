using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TripleT.Application.Common.Authorization.Models;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Application.Common.Interfaces.Utilities;
using TripleT.Domain.Entities;

namespace TripleT.Application.Common.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthenticationProvider _authenticationProvider;

        public JwtMiddleware(RequestDelegate next, IAuthenticationProvider authenticationProvider)
        {
            _next = next;
            _authenticationProvider = authenticationProvider;
        }

        public async Task Invoke(HttpContext context, ITripleTDbContext dbContext)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await AttachUserToContext(context, dbContext, token);
            }

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, ITripleTDbContext dbContext, string token)
        {
            try
            {
                JwtSecurityToken jwtToken = _authenticationProvider.ValidateJwtToken(token);
                long userId = long.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                UserEntity userEntity = await dbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == userId);

                context.Items["User"] = new AuthenticatedUser(userEntity.Id, userEntity.Role.Name);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
