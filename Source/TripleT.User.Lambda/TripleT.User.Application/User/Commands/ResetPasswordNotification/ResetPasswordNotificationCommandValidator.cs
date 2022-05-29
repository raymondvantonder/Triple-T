using FluentValidation;

namespace TripleT.User.Application.User.Commands.ResetPasswordNotification
{
    public class ResetPasswordNotificationCommandValidator : AbstractValidator<ResetPasswordNotificationCommand>
    {
        public ResetPasswordNotificationCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
