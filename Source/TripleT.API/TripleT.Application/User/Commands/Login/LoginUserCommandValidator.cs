using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.User.Commands.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(24);
        }
    }
}
