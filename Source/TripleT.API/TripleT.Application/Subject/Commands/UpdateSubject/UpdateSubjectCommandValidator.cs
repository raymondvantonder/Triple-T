using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Subject.Commands.UpdateSubject
{
    public class UpdateSubjectCommandValidator : AbstractValidator<UpdateSubjectCommand>
    {
        public UpdateSubjectCommandValidator()
        {
            RuleFor(x => x.SubjectId)
                .GreaterThan(0).WithMessage("Invalid categoryId provided");
            
            RuleFor(x => x.NewName)
                .NotNull().WithMessage("New name not provided")
                .MaximumLength(30).WithMessage("New name not allowed to be longer than 30 characters");
        }
    }
}
