using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TripleT.Application.Common.Authorization.Models;
using TripleT.Domain.Entities;

namespace TripleT.Application.Common.Interfaces.Utilities
{
    public interface IAuthenticationProvider
    {
        TokenDetails GenerateJwtToken(long userId);

        JwtSecurityToken ValidateJwtToken(string token);
    }
}
