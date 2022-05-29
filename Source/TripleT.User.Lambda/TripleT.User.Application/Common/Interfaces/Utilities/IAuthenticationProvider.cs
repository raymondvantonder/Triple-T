using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using TripleT.User.Application.Common.Authorization.Models;

namespace TripleT.User.Application.Common.Interfaces.Utilities
{
    public interface IAuthenticationProvider
    {
        TokenDetails GenerateJwtToken(Dictionary<string, string> claims, int? expiresInSeconds = null);

        JwtSecurityToken ValidateJwtToken(string token);
    }
}
