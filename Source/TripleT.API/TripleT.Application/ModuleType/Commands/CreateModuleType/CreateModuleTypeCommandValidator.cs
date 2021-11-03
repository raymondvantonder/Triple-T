using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.ModuleType.Commands.CreateModuleType
{
    public class CreateModuleTypeCommandValidator : AbstractValidator<CreateModuleTypeCommand>
    {
        public CreateModuleTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Not provided");
        }
    }
}
