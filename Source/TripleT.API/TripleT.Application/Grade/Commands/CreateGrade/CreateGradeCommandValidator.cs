using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Grade.Commands.CreateGrade
{
    public class CreateGradeCommandValidator : AbstractValidator<CreateGradeCommand>
    {
        public CreateGradeCommandValidator()
        {
            RuleFor(x => x.Grade)
                .NotEmpty().WithMessage("Not Provided");
        }
    }
}
