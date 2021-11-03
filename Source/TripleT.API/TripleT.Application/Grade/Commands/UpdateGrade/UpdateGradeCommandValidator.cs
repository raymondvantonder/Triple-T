using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Grade.Commands.UpdateGrade
{
    public class UpdateGradeCommandValidator : AbstractValidator<UpdateGradeCommand>
    {
        public UpdateGradeCommandValidator()
        {
            RuleFor(x => x.NewGrade)
                .NotEmpty().WithMessage("Not Provided");
        }
    }
}
