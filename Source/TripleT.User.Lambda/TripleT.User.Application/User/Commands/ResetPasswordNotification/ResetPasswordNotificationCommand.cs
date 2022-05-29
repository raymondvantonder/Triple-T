using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Constants;
using TripleT.User.Application.Common.Interfaces.Infrastructure;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Application.Common.Interfaces.Utilities;
using TripleT.User.Application.Common.Models.Emailing;
using TripleT.User.Application.Common.Utilities;
using TripleT.User.Domain.Domain;
using TripleT.User.Domain.Primitives;

namespace TripleT.User.Application.User.Commands.ResetPasswordNotification
{
    public class ResetPasswordNotificationCommand : IRequest
    {
        public string Email { get; set; }
    }

    public class ResetPasswordNotificationCommandHandler : IRequestHandler<ResetPasswordNotificationCommand>
    {
        private readonly ILogger<ResetPasswordNotificationCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordRepository _passwordRepository;
        private readonly IEmailingService _emailingService;
        private readonly IAuthenticationProvider _authenticationProvider;

        public ResetPasswordNotificationCommandHandler(ILogger<ResetPasswordNotificationCommandHandler> logger,
            IUserRepository userRepository,
            IPasswordRepository passwordRepository,
            IEmailingService emailingService,
            IAuthenticationProvider authenticationProvider)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordRepository = passwordRepository;
            _emailingService = emailingService;
            _authenticationProvider = authenticationProvider;
        }

        public async Task<Unit> Handle(ResetPasswordNotificationCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetItemByIdAsync(request.Email, cancellationToken);

            if (userEntity == null)
            {
                _logger.LogInformation("User does exist for password reset '{Email}'", request.Email);
                return Unit.Value;
            }

            var reference = Guid.NewGuid().ToString();

            var urlReference = ReferenceHelper.CreateReference(_authenticationProvider, userEntity.Email, reference, SystemRoles.PasswordReset);

            var emailRequest = new EmailRequest<ForgotPasswordEmailData>
            {
                Template = EmailTemplateConstants.FORGOT_PASSWORD_TEMPLATE_NAME, ToEmailAdresses = new List<string> {userEntity.Email}, Data = new ForgotPasswordEmailData
                {
                    Name = userEntity.Name, Surname = userEntity.Surname, ResetPasswordUrl = $"http://localhost:3000/reset-password?reference={urlReference}"
                }
            };

            await _emailingService.SendEmailAsync(emailRequest, cancellationToken);

            var passwordEntity = await _passwordRepository.GetItemByIdAsync(userEntity.Email, cancellationToken);

            passwordEntity.PasswordResetDetails = new PasswordResetDetails()
            {
                Reference = reference, 
                ExpiryTime = DateTime.Now.AddDays(7)
            };

            await _passwordRepository.UpdateItemAsync(passwordEntity, cancellationToken);

            return Unit.Value;
        }
    }
}