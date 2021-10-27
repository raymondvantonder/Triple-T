using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class SummaryCategoryEntity : AuditableEntity
    {
        public string Name { get; set; }

        public IList<SummaryEntity> Summaries { get; set; } = new List<SummaryEntity>();
        public IList<SummaryPackageEntity> SummaryPackages { get; set; } = new List<SummaryPackageEntity>();
    }
}
