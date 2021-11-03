using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Application.Common.Interfaces.Utilities;
using TripleT.Application.Common.Models.Emailing;
using TripleT.Application.Common.Utilities;
using TripleT.Application.Interfaces.Infrastructure;
using TripleT.Domain.Entities;
using TripleT.Domain.Exceptions;

namespace TripleT.Application.User.Commands.Register
{
    public class RegisterUserCommand : IRequest<long>
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, long>
    {
        private readonly ITripleTDbContext _context;
        private readonly IEmailingService _emailingService;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(ITripleTDbContext context, ILogger<RegisterUserCommandHandler> logger, IEmailingService emailingService)
        {
            _emailingService = emailingService;
            _context = context;
            _logger = logger;
        }

        public async Task<long> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (_context.Users.Any(x => x.Email == request.Email))
            {
                _logger.LogWarning($"Email [{request.Email}] already exists");

                throw new DuplicateEntityException($"Email [{request.Email}] already exists");
            }

            RoleEntity userRoleEntity = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "client", cancellationToken);

            var userEntity = new UserEntity
            {
                Cellphone = request.Cellphone,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                Firstname = request.Firstname,
                Surname = request.Surname,
                Role = userRoleEntity,
                Password = request.Password
            };

            await _context.Users.AddAsync(userEntity, cancellationToken);

            userEntity.Password = Convert.ToBase64String(HashUtility.CreateSHA256Hash($"{userEntity.Id}{request.Password}"));

            await _context.SaveChangesAsync(cancellationToken);

            string reference = Guid.NewGuid().ToString();

            var emailRequest = new EmailRequest<VerificationEmailData>
            {
                EmailType = Common.Models.Emailing.Enumerations.EmailTypes.VERIFICATION_EMAIL,
                ToEmailAdresses = new List<string> { userEntity.Email },
                Data = new VerificationEmailData
                {
                    Name = userEntity.Firstname,
                    Surname = userEntity.Surname,
                    VerificationUrl = $"https://localhost:44315/User/verify-email/{reference}/{userEntity.Id}"
                }
            };

            await _emailingService.SendEmailAsync(emailRequest, cancellationToken);

            userEntity.EmailVerification = new EmailVerificationEntity
            {
                Reference = reference,
                Verified = false
            };

            await _context.SaveChangesAsync(cancellationToken);

            return userEntity.Id;
        }
    }
}
