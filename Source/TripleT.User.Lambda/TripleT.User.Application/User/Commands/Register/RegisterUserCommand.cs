using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Constants;
using TripleT.User.Application.Common.Interfaces.Infrastructure;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Application.Common.Interfaces.Utilities;
using TripleT.User.Application.Common.Models.Configuration;
using TripleT.User.Application.Common.Models.Emailing;
using TripleT.User.Application.Common.Utilities;
using TripleT.User.Domain.Domain;
using TripleT.User.Domain.Exceptions;
using TripleT.User.Domain.Primitives;

namespace TripleT.User.Application.User.Commands.Register
{
    public class RegisterUserCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IEmailingService _emailingService;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly MailSettings _mailSettings;
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordRepository _passwordRepository;

        public RegisterUserCommandHandler(
            ILogger<RegisterUserCommandHandler> logger,
            IUserRepository userRepository,
            IPasswordRepository passwordRepository,
            IEmailingService emailingService,
            IAuthenticationProvider authenticationProvider,
            IConfiguration configuration)
        {
            _emailingService = emailingService;
            _authenticationProvider = authenticationProvider;
            _logger = logger;
            _userRepository = userRepository;
            _passwordRepository = passwordRepository;

            _mailSettings = new MailSettings
            {
                Enabled = bool.Parse(configuration["MAILING_ENABLED"]),
                VerificationUri = configuration["EMAIL_VERIFICATION_URL"],
                PasswordResetUri = configuration["PASSWORD_RESET_URL"],
            };
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUserEntity = await _userRepository.GetItemByIdAsync(request.Email, cancellationToken);

            if (existingUserEntity != null)
            {
                _logger.LogWarning("Email [{Email}] already exists", request.Email);

                throw new DuplicateEntityException($"Email [{request.Email}] already exists");
            }

            var userEntity = new UserEntity
            {
                Cellphone = request.Cellphone, 
                Email = request.Email, 
                Name = request.Name, 
                Surname = request.Surname, 
                DateOfBirth = request.DateOfBirth, 
                Role = UserRoles.Client
            };

            var emailVerificationReference = await SendVerificationEmail(userEntity, cancellationToken);

            userEntity.EmailVerification = new EmailVerification
            {
                Reference = emailVerificationReference, Verified = false
            };

            await _userRepository.AddItemAsync(userEntity, cancellationToken);
            
            var passwordEntity = PasswordHelper.CreatePassword(request.Password, userEntity.Email);
            
            await _passwordRepository.AddItemAsync(passwordEntity, cancellationToken);

            return Unit.Value;
        }

        private async Task<string> SendVerificationEmail(UserEntity userEntity, CancellationToken cancellationToken)
        {
            var emailVerificationReference = Guid.NewGuid().ToString();

            var urlReference = ReferenceHelper.CreateReference(_authenticationProvider, userEntity.Email, emailVerificationReference, SystemRoles.VerifyEmail);

            var verifyEmailEndpoint = _mailSettings.VerificationUri;

            var emailRequest = new EmailRequest<VerificationEmailData>
            {
                Template = EmailTemplateConstants.VERIFICATION_TEMPLATE_NAME, ToEmailAdresses = new List<string> {userEntity.Email}, Data = new VerificationEmailData
                {
                    Name = userEntity.Name, Surname = userEntity.Surname, VerificationUrl = string.Format(verifyEmailEndpoint, urlReference)
                }
            };

            await _emailingService.SendEmailAsync(emailRequest, cancellationToken);

            return emailVerificationReference;
        }
    }
}