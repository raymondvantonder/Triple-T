using FluentValidation;

namespace TripleT.User.Application.Subject.Commands.CreateSubject
{
    public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}