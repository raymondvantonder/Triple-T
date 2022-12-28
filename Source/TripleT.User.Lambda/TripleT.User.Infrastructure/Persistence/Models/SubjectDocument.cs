namespace TripleT.User.Infrastructure.Persistence.Models
{
    public class SubjectDocument : DocumentBase<SubjectDocument>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}