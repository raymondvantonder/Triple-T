using System.Collections.Generic;

namespace TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsBySubjectAndGrade
{
    public class GetSummaryProductsBySubjectAndGradeQueryViewModel
    {
        public IEnumerable<SummaryProductDto> Products { get; set; }
    }
}