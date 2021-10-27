using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Summary.Commands.CreateSummary
{
    public class CreateSummaryCommandValidator : AbstractValidator<CreateSummaryCommand>
    {
        public CreateSummaryCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId not provided");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be null")
                .MaximumLength(30).WithMessage("Name cannot be longer than 30 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be null")
                .MaximumLength(250).WithMessage("Description cannot be longer than 30 characters");

            RuleFor(x => x.FileLocation)
                .NotEmpty().WithMessage("FileLocation cannot be null")
                .MaximumLength(100).WithMessage("FileLocation cannot be longer than 100 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price not provided");
        }
    }
}
