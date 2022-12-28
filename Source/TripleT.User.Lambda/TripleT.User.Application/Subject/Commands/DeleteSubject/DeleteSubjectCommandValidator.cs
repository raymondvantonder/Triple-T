using FluentValidation;

namespace TripleT.User.Application.Subject.Commands.DeleteSubject
{
    public class DeleteSubjectCommandValidator : AbstractValidator<DeleteSubjectCommand>
    {
        public DeleteSubjectCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}