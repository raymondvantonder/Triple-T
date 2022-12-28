using TripleT.User.Infrastructure.Persistence.Models.Interfaces;

namespace TripleT.User.Infrastructure.Persistence.Models
{
    public class SummaryProductDocument : DocumentBase<SummaryProductDocument>, IGlobalSecondaryIndex1, IGlobalSecondaryIndex2
    {
        public string GSI1PK { get; set; } 
        public string GSI1SK { get; set; }
        public string GSI2PK { get; set; }
        public string GSI2SK { get; set; }

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