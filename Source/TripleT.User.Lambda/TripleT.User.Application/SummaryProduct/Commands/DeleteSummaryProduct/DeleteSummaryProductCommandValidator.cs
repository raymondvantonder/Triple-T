using FluentValidation;

namespace TripleT.User.Application.SummaryProduct.Commands.DeleteSummaryProduct
{
    public class DeleteSummaryProductCommandValidator : AbstractValidator<DeleteSummaryProductCommand>
    {
        public DeleteSummaryProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}