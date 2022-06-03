using FluentValidation;

namespace TripleT.User.Application.Grade.Commands.UpdateGrade
{
    public class UpdateGradeCommandValidator : AbstractValidator<UpdateGradeCommand>
    {
        public UpdateGradeCommandValidator()
        {
            RuleFor(x => x.Id)
                .MaximumLength(200)
                .NotEmpty();
        
            RuleFor(x => x.NewName)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}