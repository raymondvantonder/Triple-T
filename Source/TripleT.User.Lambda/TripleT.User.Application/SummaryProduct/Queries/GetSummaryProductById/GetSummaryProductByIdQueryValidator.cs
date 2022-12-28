using FluentValidation;

namespace TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductById
{
    public class GetSummaryProductByIdQueryValidator : AbstractValidator<GetSummaryProductByIdQuery>
    {
        public GetSummaryProductByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}