using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;

namespace TripleT.Application.User.Commands.ResetPassword
{
    public class ResetPasswordNotificationCommand : IRequest
    {
        public string Email { get; set; }
    }

    public class ResetPasswordNotificationCommandHandler : IRequestHandler<ResetPasswordNotificationCommand>
    {
        private readonly ILogger<ResetPasswordNotificationCommandHandler> _logger;
        //private readonly ITrippleTSmtpClient _smtpClient;
        private readonly ITripleTDbContext _context;

        public ResetPasswordNotificationCommandHandler(ILogger<ResetPasswordNotificationCommandHandler> logger, ITripleTDbContext context)
        {
            //_smtpClient = smtpClient;
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(ResetPasswordNotificationCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user == null)
            {
                _logger.LogInformation($"User does exist for password reset '{request.Email}'");
                return Unit.Value;
            }

            string reference = null;//await _smtpClient.SendPasswordResetMailAsync(user.Email, cancellationToken);

            user.PasswordResetDetail = new PasswordResetDetailEntity
            {
                Reference = reference,
                ExpiryTime = DateTime.Now.AddDays(7)
            };

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
