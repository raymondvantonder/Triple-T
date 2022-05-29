using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.User.Commands.Login;
using TripleT.User.Application.User.Commands.Register;
using TripleT.User.Application.User.Commands.ResetPassword;
using TripleT.User.Application.User.Commands.ResetPasswordNotification;
using TripleT.User.Application.User.Commands.Update;
using TripleT.User.Application.User.Commands.VerifyEmail;

namespace TripleT.User.Lambda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<LoginUserViewModel> LoginUserAsync([FromBody] LoginUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Login started");
            try
            {
                return await _mediator.Send(request, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError($"Login Failed: {e}");
                throw;
            }
            finally
            {
                _logger.LogInformation("Login Completed");
            }
        }

        [HttpPost("register")]
        public async Task RegisterUserAsync([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }

        [HttpPut("verify-email")]
        public async Task VerifyEmailAsync(CancellationToken cancellationToken)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString().Split(" ").Last();
            await _mediator.Send(new VerifyEmailCommand {Token = token}, cancellationToken);
        }

        [HttpPut("reset-password-notification")]
        public async Task ResetPasswordNotification([FromBody] ResetPasswordNotificationCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }

        [HttpPut("reset-password")]
        public async Task ResetPasswordAsync([FromBody] ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            command.Token = HttpContext.Request.Headers.Authorization.ToString().Split(" ").Last();

            await _mediator.Send(command, cancellationToken);
        }

        [HttpPut("update")]
        public async Task UpdateUserAsync([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }
    }
}