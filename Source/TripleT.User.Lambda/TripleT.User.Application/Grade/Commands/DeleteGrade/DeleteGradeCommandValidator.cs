using FluentValidation;

namespace TripleT.User.Application.Grade.Commands.DeleteGrade
{
    public class DeleteGradeCommandValidator : AbstractValidator<DeleteGradeCommand>
    {
        public DeleteGradeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}