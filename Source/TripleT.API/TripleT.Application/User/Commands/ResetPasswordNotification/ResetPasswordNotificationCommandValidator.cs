using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.User.Commands.ResetPassword
{
    public class ResetPasswordNotificationCommandValidator : AbstractValidator<ResetPasswordNotificationCommand>
    {
        public ResetPasswordNotificationCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
