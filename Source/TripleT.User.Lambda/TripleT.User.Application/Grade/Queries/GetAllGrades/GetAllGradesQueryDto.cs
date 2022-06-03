using System;

namespace TripleT.User.Application.Grade.Queries.GetAllGrades
{
    public class GetAllGradesQueryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}