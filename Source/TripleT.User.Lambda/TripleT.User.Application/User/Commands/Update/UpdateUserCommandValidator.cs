using FluentValidation;

namespace TripleT.User.Application.User.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(x => x.Surname)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(x => x.Cellphone)
                .Length(10);
        }
    }
}
