namespace TripleT.User.Domain.Domain
{
    public class SummaryProductEntity : EntityBase
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileLocation { get; set; }
        public decimal FileSizeInMb { get; set; }
        public string IconUri { get; set; }
        public decimal Price { get; set; }
        public string GradeId { get; set; }
        public string SubjectId { get; set; }
    }
}