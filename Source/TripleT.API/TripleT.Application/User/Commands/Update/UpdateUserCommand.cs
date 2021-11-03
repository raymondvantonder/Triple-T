using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Application.Interfaces;
using TripleT.Domain.Entities;
using TripleT.Domain.Exceptions;

namespace TripleT.Application.User.Commands.Update
{
    public class UpdateUserCommand : IRequest
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Cellphone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly ITripleTDbContext _context;

        public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity userEntity = await _context.Users.FindAsync(request.Id);

            if (userEntity == null)
            {
                _logger.LogWarning($"User not found {request.Id}");

                throw new EntityNotFoundException($"User not found: [{request.Id}]");
            }

            userEntity.Firstname = request.Firstname;
            userEntity.Surname = request.Surname;
            userEntity.DateOfBirth = request.DateOfBirth;
            userEntity.Cellphone = request.Cellphone;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
