using System.Data;
using FluentValidation;

namespace TripleT.User.Application.SummaryProduct.Commands.CreateSummaryProduct
{
    public class CreateSummaryProductCommandValidator : AbstractValidator<CreateSummaryProductCommand>
    {
        public CreateSummaryProductCommandValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(x => x.Title)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(x => x.Price)
                .GreaterThan(0);
            
            RuleFor(x => x.FileLocation)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(x => x.GradeId)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(x => x.IconUri)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(x => x.SubjectId)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(x => x.FileSizeInMb)
                .LessThan(1000)
                .GreaterThan(0);
        }
    }
}