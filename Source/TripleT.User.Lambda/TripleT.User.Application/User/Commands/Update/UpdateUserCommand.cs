using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Exceptions;

namespace TripleT.User.Application.User.Commands.Update
{
    public class UpdateUserCommand : IRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Cellphone { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetItemByIdAsync(request.Email, cancellationToken);

            if (userEntity == null)
            {
                _logger.LogWarning("User not found {Email}", request.Email);

                throw new EntityNotFoundException($"User not found: [{request.Email}]");
            }

            userEntity.Name = request.Name;
            userEntity.Surname = request.Surname;
            userEntity.Cellphone = request.Cellphone;

            await _userRepository.UpdateItemAsync(userEntity, cancellationToken);

            return Unit.Value;
        }
    }
}
