using System.Collections.Generic;
using TripleT.User.Application.Common.Authorization.Models;
using TripleT.User.Application.Common.Interfaces.Utilities;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Common.Authorization.Extensions
{
    public static class AuthenticationProviderExtensions
    {
        public static TokenDetails GenerateJwtTokenForUser(this IAuthenticationProvider authenticationProvider, UserEntity userEntity)
        {
            var claims = GetClaims(userEntity);

            return authenticationProvider.GenerateJwtToken(claims);
        }

        private static Dictionary<string, string> GetClaims(UserEntity userEntity)
        {
            return new Dictionary<string, string>
            {
                ["Email"] = userEntity.Email,
                ["Role"] = userEntity.Role.ToString()
            };
        }
    }
}