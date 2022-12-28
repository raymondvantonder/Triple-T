using System.Collections.Generic;

namespace TripleT.User.Application.Subject.Queries.GetAllSubjects
{
    public class GetAllSubjectsViewModel
    {
        public IEnumerable<GetAllSubjectsQueryDto> Subjects { get; set; }
    }
}