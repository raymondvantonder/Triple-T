using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Application.Common.Interfaces.Utilities;
using TripleT.User.Application.Common.Utilities;

namespace TripleT.User.Application.User.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest
    {
        public string Token { get; set; }
    }

    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand>
    {
        private readonly ILogger<VerifyEmailCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationProvider _authenticationProvider;

        public VerifyEmailCommandHandler(ILogger<VerifyEmailCommandHandler> logger, 
            IUserRepository userRepository,
            IAuthenticationProvider authenticationProvider)
        {
            _logger = logger;
            _userRepository = userRepository;
            _authenticationProvider = authenticationProvider;
        }

        public async Task<Unit> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var referenceData = ReferenceHelper.GetDetailsFromReference(_authenticationProvider, request.Token);

            var userEntity = await _userRepository.GetItemByIdAsync(referenceData.Email, cancellationToken);

            if (userEntity == null)
            {
                throw new UnauthorizedAccessException($"Invalid reference code [{referenceData.Reference}] provided for email validation.");
            }

            if (userEntity.EmailVerification.Verified)
            {
                _logger.LogInformation($"Email already verified");
                return Unit.Value;
            }

            if (userEntity.EmailVerification.Reference != referenceData.Reference)
            {
                _logger.LogWarning("Reference provided in email verification does not match: [{Reference}]", referenceData.Reference);
                throw new UnauthorizedAccessException($"Invalid reference code [{referenceData.Reference}] provided for email validation.");
            }
            
            userEntity.EmailVerification.Verified = true;
            userEntity.EmailVerification.VerifiedTime = DateTime.Now;

            await _userRepository.UpdateItemAsync(userEntity, cancellationToken);

            _logger.LogInformation($"Email verified");

            return Unit.Value;

        }
    }
}
