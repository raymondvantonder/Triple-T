using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Grade.Commands.DeleteGrade
{
    public class DeleteGradeCommandValidator : AbstractValidator<DeleteGradeCommand>
    {
        public DeleteGradeCommandValidator()
        {
            RuleFor(x => x.GradeId).GreaterThan(0).WithMessage("Not provided");
        }
    }
}
