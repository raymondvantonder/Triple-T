using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Summary.Commands.DeleteSummary
{
    public class DeleteSummaryCommandValidator : AbstractValidator<DeleteSummaryCommand>
    {
        public DeleteSummaryCommandValidator()
        {
            RuleFor(x => x.SummaryId)
                .GreaterThan(0).WithMessage("SummaryId not provided");
        }
    }
}
