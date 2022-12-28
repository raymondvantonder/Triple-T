using System.Data;
using FluentValidation;

namespace TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsBySubject
{
    public class GetSummaryProductsBySubjectQueryValidator : AbstractValidator<GetSummaryProductsBySubjectQuery>
    {
        public GetSummaryProductsBySubjectQueryValidator()
        {
            RuleFor(x => x.SubjectId)
                .MaximumLength(36)
                .NotEmpty();
        }
    }
}