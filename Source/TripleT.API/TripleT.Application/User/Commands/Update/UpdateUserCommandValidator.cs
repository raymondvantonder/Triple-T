using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.User.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Cellphone).Length(10);
            RuleFor(x => x.DateOfBirth).NotEqual(default(DateTime));
        }
    }
}
