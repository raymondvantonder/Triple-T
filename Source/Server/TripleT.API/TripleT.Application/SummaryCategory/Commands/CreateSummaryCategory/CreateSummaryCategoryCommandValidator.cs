using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.SummaryCategory.Commands.CreateSummaryCategory
{
    public class CreateSummaryCategoryCommandValidator : AbstractValidator<CreateSummaryCategoryCommand>
    {
        public CreateSummaryCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name not provided")
                .MaximumLength(30).WithMessage("name not allowed to be longer than 30 characters");
        }
    }
}
