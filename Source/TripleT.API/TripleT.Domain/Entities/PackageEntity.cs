using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class PackageEntity : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Foreign keys
        public long SubjectId { get; set; }
        public long GradeId { get; set; }

        public SubjectEntity Subject { get; set; }
        public GradeEntity Grade { get; set; }
        public IList<ModuleEntity> Modules { get; set; } = new List<ModuleEntity>();
    }
}
