using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.User.Commands.VerifyEmail
{
    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Reference).NotNull();
        }
    }
}
