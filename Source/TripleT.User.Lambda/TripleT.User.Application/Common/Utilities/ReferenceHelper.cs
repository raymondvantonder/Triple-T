using System.Collections.Generic;
using System.Linq;
using TripleT.User.Application.Common.Interfaces.Utilities;
using TripleT.User.Domain.Primitives;

namespace TripleT.User.Application.Common.Utilities
{
    public static class ReferenceHelper
    {
        public static (string Email, string Reference) GetDetailsFromReference(IAuthenticationProvider authenticationProvider, string token)
        {
            var jwtToken = authenticationProvider.ValidateJwtToken(token);

            var email = jwtToken.Claims.Single(x => x.Type == "Email").Value;
            var reference = jwtToken.Claims.Single(x => x.Type == "Reference").Value;
            
            return (email, reference);
        }
    
        public static string CreateReference(IAuthenticationProvider authenticationProvider, string email, string reference, SystemRoles role)
        {
            var claims = new Dictionary<string, string>
            {
                ["Email"] = email, 
                ["Reference"] = reference,
                ["Role"] = role.ToString()
            };
            
            return authenticationProvider.GenerateJwtToken(claims, 7 * 86400).Token;
        }
    }
}