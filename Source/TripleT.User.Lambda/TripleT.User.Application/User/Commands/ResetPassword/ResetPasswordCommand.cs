using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Application.Common.Interfaces.Utilities;
using TripleT.User.Application.Common.Utilities;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.User.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        [JsonIgnore]
        public string Token { get; set; }
        public string Password { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly ILogger<ResetPasswordCommandHandler> _logger;
        private readonly IPasswordRepository _passwordRepository;
        private readonly IEmailingService _emailingService;
        private readonly IAuthenticationProvider _authenticationProvider;

        public ResetPasswordCommandHandler(ILogger<ResetPasswordCommandHandler> logger,
            IPasswordRepository passwordRepository,
            IEmailingService emailingService,
            IAuthenticationProvider authenticationProvider)
        {
            _logger = logger;
            _passwordRepository = passwordRepository;
            _emailingService = emailingService;
            _authenticationProvider = authenticationProvider;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var referenceDetails = ReferenceHelper.GetDetailsFromReference(_authenticationProvider, request.Token);

            var passwordEntity = await _passwordRepository.GetItemByIdAsync(referenceDetails.Email, cancellationToken);

            Guard.Against.Null(passwordEntity.PasswordResetDetails, nameof(PasswordEntity.PasswordResetDetails));
            
            if (passwordEntity.PasswordResetDetails.Reference != referenceDetails.Reference)
            {
                _logger.LogError($"Unique Reference: [{referenceDetails.Reference}] provided for update password does not match");
                return Unit.Value;
            }

            if (DateTime.Now > passwordEntity.PasswordResetDetails.ExpiryTime)
            {
                _logger.LogError($"Password reset expired at : [{passwordEntity.PasswordResetDetails.ExpiryTime}] for Unique Reference: [{referenceDetails.Reference}]");
                return Unit.Value;
            }

            var newPasswordEntity = PasswordHelper.CreatePassword(request.Password, passwordEntity.Email);
            
            await _passwordRepository.UpdateItemAsync(newPasswordEntity, cancellationToken);

            _logger.LogInformation($"Password updated successfully");

            //_emailingService.SendEmailAsync(EmailTemplateConstants.PASSWORD_RESET_SUCCESSFULLY); TODO

            return Unit.Value;
        }
    }
}