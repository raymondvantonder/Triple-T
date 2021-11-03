using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class EmailVerificationEntity : AuditableEntity
    {
        public string Reference { get; set; }
        public bool Verified { get; set; }
        public DateTime? VerifiedTime { get; set; }
    }
}
