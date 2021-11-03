using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Language.Commands.CreateLanguage
{
    public class CreateLanguageCommandValidator : AbstractValidator<CreateLanguageCommand>
    {
        public CreateLanguageCommandValidator()
        {
            RuleFor(x => x.Language)
                .NotEmpty()
                .WithMessage("Not provided")
                .MaximumLength(30).WithMessage("Not allowed to be longer than 30 characters");
        }
    }
}
