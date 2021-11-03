using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TripleT.Application.Common.Authorization.Models;
using TripleT.Application.Common.Interfaces.Utilities;

namespace TripleT.Application.Common.Authorization
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<AuthenticationProvider> _logger;

        public AuthenticationProvider(ILogger<AuthenticationProvider> logger, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public TokenDetails GenerateJwtToken(long userId)
        {
            // generate token that is valid for 7 days
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddSeconds(_appSettings.ExpiresInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenDetails
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresInSeconds = _appSettings.ExpiresInSeconds
            };
        }

        public JwtSecurityToken ValidateJwtToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to validate jwt token: {e}");
                throw;
            }
        }
    }
}
