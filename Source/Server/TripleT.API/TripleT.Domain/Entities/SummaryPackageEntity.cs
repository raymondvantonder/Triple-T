using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class SummaryPackageEntity : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Foreign keys
        public long SummaryCategoryId { get; set; }

        public SummaryCategoryEntity SummaryCategory { get; set; }
        public IList<PackageSummaryLinkEntity> PackageSummaryLinks { get; set; } = new List<PackageSummaryLinkEntity>();
    }
}
