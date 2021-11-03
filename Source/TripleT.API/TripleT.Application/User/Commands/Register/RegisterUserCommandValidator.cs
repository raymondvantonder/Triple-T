using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.User.Commands.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Cellphone).Length(10);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.DateOfBirth).NotEqual(default(DateTime));
            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(24);
        }
    }
}
