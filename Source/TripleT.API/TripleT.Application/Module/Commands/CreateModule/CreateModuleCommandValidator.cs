using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Module.Commands.CreateModule
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator()
        {
            RuleFor(x => x.SubjectId)
                .GreaterThan(0).WithMessage("Not provided");

            RuleFor(x => x.ModuleTypeId)
                .GreaterThan(0).WithMessage("Not provided");

            RuleFor(x => x.GradeId)
                .GreaterThan(0).WithMessage("Not provided");

            RuleFor(x => x.LanguageId)
                .GreaterThan(0).WithMessage("Not provided");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be null")
                .MaximumLength(30).WithMessage("Cannot be longer than 30 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be null")
                .MaximumLength(250).WithMessage("Cannot be longer than 30 characters");

            RuleFor(x => x.FileLocation)
                .NotEmpty().WithMessage("FileLocation cannot be null")
                .MaximumLength(100).WithMessage("Cannot be longer than 100 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Not provided");
        }
    }
}
