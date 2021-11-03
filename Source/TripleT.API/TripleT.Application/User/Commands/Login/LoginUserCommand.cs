using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Authorization.Models;
using TripleT.Application.Common.Exceptions;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Application.Common.Interfaces.Utilities;
using TripleT.Application.Common.Utilities;
using TripleT.Domain.Entities;

namespace TripleT.Application.User.Commands.Login
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly ILogger<LoginUserCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITripleTDbContext _context;
        private readonly IAuthenticationProvider _authenticationProvider;

        public LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger, ITripleTDbContext context, IMapper mapper, IAuthenticationProvider authenticationProvider)
        {
            _authenticationProvider = authenticationProvider;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity userEntity = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == request.Email);

            if (userEntity == null)
            {
                _logger.LogWarning($"User not found [{request.Email}]");

                throw new AuthenticationFailedException();
            }

            string passwordHash = Convert.ToBase64String(HashUtility.CreateSHA256Hash($"{userEntity.Id}{request.Password}"));

            if (userEntity.Password != passwordHash)
            {
                _logger.LogWarning($"Invalid password for {request.Email}");

                throw new AuthenticationFailedException();
            }

            TokenDetails tokenDetails = _authenticationProvider.GenerateJwtToken(userEntity.Id);

            return new LoginUserViewModel
            {
                User = _mapper.MapMultiple<LoginUserDto>(userEntity, tokenDetails)
            };
        }
    }
}
