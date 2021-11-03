using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class SubjectEntity : AuditableEntity
    {
        public string Name { get; set; }

        public IList<ModuleEntity> Modules { get; set; } = new List<ModuleEntity>();
        public IList<PackageEntity> Packages { get; set; } = new List<PackageEntity>();
    }
}
