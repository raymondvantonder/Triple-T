using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Domain.Entities.Common
{
    public class AuditableEntity : Entity
    {
        public DateTime CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}
