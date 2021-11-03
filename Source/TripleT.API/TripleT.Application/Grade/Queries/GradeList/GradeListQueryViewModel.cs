using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Grade.Queries.GradeList
{
    public class GradeListQueryViewModel
    {
        public IEnumerable<GradeListQueryDto> Grades { get; set; }
    }
}
