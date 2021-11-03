using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Language.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageCommandValidator()
        {
            RuleFor(x => x.LanguageId)
                .GreaterThan(0).WithMessage("Invalid categoryId provided");

            RuleFor(x => x.NewLanguage)
                .NotNull().WithMessage("New name not provided")
                .MaximumLength(30).WithMessage("New name not allowed to be longer than 30 characters");
        }
    }
}
