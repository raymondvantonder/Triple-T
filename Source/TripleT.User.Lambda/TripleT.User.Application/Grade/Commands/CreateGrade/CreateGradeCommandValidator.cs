using FluentValidation;

namespace TripleT.User.Application.Grade.Commands.CreateGrade
{
    public class CreateGradeCommandValidator : AbstractValidator<CreateGradeCommand>
    {
        public CreateGradeCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}