using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class PackageSummaryLinkEntity : AuditableEntity
    {
        public long SummaryId { get; set; }
        public long SummaryPackageId { get; set; }

        //Foreign Keys
        public SummaryPackageEntity SummaryPackage { get; set; }
        public SummaryEntity Summary { get; set; }
    }
}
