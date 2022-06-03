using FluentValidation;

namespace TripleT.User.Application.User.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Password)
                .MaximumLength(24)
                .MinimumLength(8)
                .NotEmpty();
            
            RuleFor(x => x.Token)
                .MaximumLength(400)
                .NotEmpty();
        }
    }
}
