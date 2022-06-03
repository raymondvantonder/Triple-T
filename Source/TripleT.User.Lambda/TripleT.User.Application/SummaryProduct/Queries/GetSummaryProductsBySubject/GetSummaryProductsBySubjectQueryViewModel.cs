using System.Collections.Generic;

namespace TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsBySubject
{
    public class GetSummaryProductsBySubjectQueryViewModel
    {
        public IEnumerable<SummaryProductDto> Products { get; set; }
    }
}