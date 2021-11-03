using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Module.Commands.DeleteModule
{
    public class DeleteModuleCommandValidator : AbstractValidator<DeleteModuleCommand>
    {
        public DeleteModuleCommandValidator()
        {
            RuleFor(x => x.ModuleId)
                .GreaterThan(0).WithMessage("Not provided");
        }
    }
}
