using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.SummaryCategory.Commands.UpdateSummaryCategory
{
    public class UpdateSummaryCategoryCommandValidator : AbstractValidator<UpdateSummaryCategoryCommand>
    {
        public UpdateSummaryCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Invalid categoryId provided");
            
            RuleFor(x => x.NewName)
                .NotNull().WithMessage("New name not provided")
                .MaximumLength(30).WithMessage("New name not allowed to be longer than 30 characters");
        }
    }
}
