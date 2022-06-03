using System.Collections.Generic;

namespace TripleT.User.Application.Grade.Queries.GetAllGrades
{
    public class GetAllGradesViewModel
    {
        public IEnumerable<GetAllGradesQueryDto> Grades { get; set; }
    }
}