using FluentValidation;

namespace TripleT.User.Application.Subject.Commands.UpdateSubject
{
    public class UpdateSubjectCommandValidator : AbstractValidator<UpdateSubjectCommand>
    {
        public UpdateSubjectCommandValidator()
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