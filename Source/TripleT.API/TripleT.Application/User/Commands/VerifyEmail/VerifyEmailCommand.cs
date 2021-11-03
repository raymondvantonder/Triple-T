using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Extensions;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;
using TripleT.Domain.Exceptions;

namespace TripleT.Application.User.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest
    {
        public long UserId { get; set; }
        public string Reference { get; set; }
    }

    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand>
    {
        private readonly ITripleTDbContext _context;
        private readonly ILogger<VerifyEmailCommandHandler> _logger;

        public VerifyEmailCommandHandler(ITripleTDbContext context, ILogger<VerifyEmailCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            UserEntity userEntity = await _context.Users.Include(x => x.EmailVerification).FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (userEntity == null)
            {
                throw new EntityNotFoundException($"Could not find entity for: [{request.UserId}]");
            }

            if (userEntity.EmailVerification.Verified)
            {
                _logger.LogInformation($"Email already verified");
                return Unit.Value;
            }

            if (userEntity.EmailVerification.Reference == request.Reference)
            {
                userEntity.EmailVerification.Verified = true;
                userEntity.EmailVerification.VerifiedTime = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Email verified");

                return Unit.Value;
            }

            _logger.LogWarning($"Failed to verify {request.FormatAsJsonForLogging()}");
            throw new UnauthorizedAccessException("Invalid reference code provided for email validation");

        }
    }
}
