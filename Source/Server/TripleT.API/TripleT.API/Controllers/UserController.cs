using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Authorization;
using TripleT.Application.User.Commands.Login;
using TripleT.Application.User.Commands.Register;
using TripleT.Application.User.Commands.ResetPassword;
using TripleT.Application.User.Commands.Update;
using TripleT.Application.User.Commands.VerifyEmail;

namespace TripleT.API.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("login")]
        public Task<LoginUserViewModel> LoginUserAsync([FromBody] LoginUserCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(request);
        }

        [HttpPost("register")]
        public Task<long> RegisterUserAsync([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command);
        }

        [HttpGet("verify-email/{reference}/{id}")]
        public Task VerifyEmailAsync([FromRoute] string reference, [FromRoute] long id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(new VerifyEmailCommand { Reference = reference, UserId = id });
        }

        [HttpPut("reset-password-notification")]
        public Task ResetPasswordNotification([FromBody] ResetPasswordNotificationCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command);
        }

        [Authorize]
        [HttpPut("update")]
        public Task UpdateUserAsync([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command);
        }
    }
}
