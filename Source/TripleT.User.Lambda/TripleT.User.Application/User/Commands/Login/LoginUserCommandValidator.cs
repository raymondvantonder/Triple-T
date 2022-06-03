using FluentValidation;

namespace TripleT.User.Application.User.Commands.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .MaximumLength(200)
                .EmailAddress();
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(24);
        }
    }
}
