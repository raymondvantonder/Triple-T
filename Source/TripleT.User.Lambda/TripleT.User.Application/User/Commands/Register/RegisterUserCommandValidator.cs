using FluentValidation;

namespace TripleT.User.Application.User.Commands.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Cellphone)
                .Must(x => x.StartsWith("0"))
                .Length(10);

            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(24);
        }
    }
}
