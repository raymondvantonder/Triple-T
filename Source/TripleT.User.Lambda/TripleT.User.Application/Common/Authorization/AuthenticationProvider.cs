using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TripleT.User.Application.Common.Authorization.Models;
using TripleT.User.Application.Common.Interfaces.Utilities;
using TripleT.User.Application.Common.Models.Configuration;

namespace TripleT.User.Application.Common.Authorization
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly TokenSettings _tokenSettings;
        private readonly ILogger<AuthenticationProvider> _logger;

        public AuthenticationProvider(ILogger<AuthenticationProvider> logger, IConfiguration configuration)
        {
            _logger = logger;
            _tokenSettings = new TokenSettings
            {
                Secret = configuration["JWT_TOKEN_SECRET"],
                ExpiresInSeconds = int.Parse(configuration["JWT_TOKEN_EXPIRATION_SECONDS"])
            };
        }

        public TokenDetails GenerateJwtToken(Dictionary<string, string> claims, int? expiresInSeconds = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetClaims(claims)),
                Expires = DateTime.UtcNow.AddSeconds(expiresInSeconds ?? _tokenSettings.ExpiresInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenDetails
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresInSeconds = _tokenSettings.ExpiresInSeconds
            };
        }

        public JwtSecurityToken ValidateJwtToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to validate jwt token: {e}");
                throw;
            }
        }
        
        private IEnumerable<Claim> GetClaims(Dictionary<string, string> claims)
        {
            return claims.Select(x => new Claim(x.Key, x.Value));
        }
    }
}
