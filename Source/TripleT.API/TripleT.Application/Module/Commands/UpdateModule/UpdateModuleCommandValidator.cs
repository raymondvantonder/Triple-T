using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Module.Commands.UpdateModule
{
    public class UpdateModuleCommandValidator : AbstractValidator<UpdateModuleCommand>
    {
        public UpdateModuleCommandValidator()
        {
            RuleFor(x => x.ModuleId)
                .GreaterThan(0).WithMessage("CategoryId not provided");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Cannot be null")
                .MaximumLength(30).WithMessage("Cannot be longer than 30 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Cannot be null")
                .MaximumLength(250).WithMessage("Cannot be longer than 30 characters");

            RuleFor(x => x.FileLocation)
                .NotEmpty().WithMessage("Cannot be null")
                .MaximumLength(100).WithMessage("Cannot be longer than 100 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Not provided");

            RuleFor(x => x.LanguageId)
                .GreaterThan(0).WithMessage("Not provided")
                .When(x => x.LanguageId.HasValue);

            RuleFor(x => x.GradeId)
                .GreaterThan(0).WithMessage("Not provided")
                .When(x => x.GradeId.HasValue);

            RuleFor(x => x.ModuleTypeId)
                .GreaterThan(0).WithMessage("Not provided")
                .When(x => x.ModuleTypeId.HasValue);

            RuleFor(x => x.SubjectId)
                .GreaterThan(0).WithMessage("Not provided")
                .When(x => x.SubjectId.HasValue);
        }
    }
}
