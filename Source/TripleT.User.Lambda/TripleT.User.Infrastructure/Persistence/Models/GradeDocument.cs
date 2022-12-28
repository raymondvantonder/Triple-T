namespace TripleT.User.Infrastructure.Persistence.Models
{
    public class GradeDocument : DocumentBase<GradeDocument>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}