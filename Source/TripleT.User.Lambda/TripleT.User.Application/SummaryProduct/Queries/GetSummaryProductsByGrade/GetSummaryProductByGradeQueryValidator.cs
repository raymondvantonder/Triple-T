using FluentValidation;

namespace TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsByGrade
{
    public class GetSummaryProductsByGradeQueryValidator : AbstractValidator<GetSummaryProductsByGradeQuery>
    {
        public GetSummaryProductsByGradeQueryValidator()
        {
            RuleFor(x => x.GradeId)
                .MaximumLength(36)
                .NotEmpty();
        }
    }
}