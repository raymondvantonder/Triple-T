using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class ModuleEntity : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string FileLocation { get; set; }

        //Foreign Keys
        public long SubjectId { get; set; }
        public long GradeId { get; set; }
        public long ModuleTypeId { get; set; }
        public long LanguageId { get; set; }

        public SubjectEntity Subject { get; set; }
        public GradeEntity Grade { get; set; }
        public ModuleTypeEntity ModuleType { get; set; }
        public LanguageEntity Language { get; set; }
        public IList<PackageEntity> Packages { get; set; } = new List<PackageEntity>();
    }
}
