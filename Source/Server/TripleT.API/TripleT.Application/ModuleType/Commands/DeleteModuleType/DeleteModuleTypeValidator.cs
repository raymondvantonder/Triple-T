using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.ModuleType.Commands.DeleteModuleType
{
    public class DeleteModuleTypeValidator : AbstractValidator<DeleteModuleTypeCommand>
    {
        public DeleteModuleTypeValidator()
        {
            RuleFor(x => x.ModuleTypeId)
                .GreaterThan(0)
                .WithMessage("Not Provided");
        }
    }
}
