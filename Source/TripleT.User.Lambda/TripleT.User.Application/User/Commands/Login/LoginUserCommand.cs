using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Authorization.Extensions;
using TripleT.User.Application.Common.Exceptions;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Application.Common.Interfaces.Utilities;

namespace TripleT.User.Application.User.Commands.Login
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly ILogger<LoginUserCommandHandler> _logger;
        private readonly IPasswordRepository _passwordRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationProvider _authenticationProvider;

        public LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger,
            IPasswordRepository passwordRepository,
            IUserRepository userRepository,
            IAuthenticationProvider authenticationProvider)
        {
            _authenticationProvider = authenticationProvider;
            _logger = logger;
            _passwordRepository = passwordRepository;
            _userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var passwordEntity = await _passwordRepository.GetItemByIdAsync(request.Email, cancellationToken);

            if (passwordEntity == null)
            {
                _logger.LogWarning("User not found [{Email}]", request.Email);

                throw new AuthenticationFailedException();
            }

            if (!PasswordHelper.VerifyPassword(passwordEntity, request.Password))
            {
                _logger.LogWarning("Invalid password for [{Email}]", request.Email);

                throw new AuthenticationFailedException();
            }

            var userEntity = await _userRepository.GetItemByIdAsync(request.Email, cancellationToken);
            
            var tokenDetails = _authenticationProvider.GenerateJwtTokenForUser(userEntity);

            return new LoginUserViewModel
            {
                User = new LoginUserDto
                {
                    Cellphone = userEntity.Cellphone,
                    Email = userEntity.Email,
                    Name = userEntity.Name,
                    Role = userEntity.Role,
                    Surname = userEntity.Surname,
                    Token = tokenDetails.Token,
                    EmailVerified = userEntity.EmailVerification.Verified,
                    DateOfBirth = userEntity.DateOfBirth,
                    TokenExpiresInSeconds = tokenDetails.ExpiresInSeconds
                }
            };
        }
    }
}