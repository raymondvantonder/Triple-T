using System.Collections.Generic;

namespace TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsByGrade
{
    public class GetSummaryProductsByGradeQueryViewModel
    {
        public IEnumerable<SummaryProductDto> Products { get; set; }
    }
}