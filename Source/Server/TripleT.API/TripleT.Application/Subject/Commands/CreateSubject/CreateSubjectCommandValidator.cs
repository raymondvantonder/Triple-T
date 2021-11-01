using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Subject.Commands.CreateSubject
{
    public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectCommandValidator()
        {
            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage("Not provided")
                .MaximumLength(30).WithMessage("Not allowed to be longer than 30 characters");
        }
    }
}
