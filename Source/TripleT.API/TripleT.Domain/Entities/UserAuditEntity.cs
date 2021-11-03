using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class UserAuditEntity : AuditableEntity
    {
        public string Action { get; set; }
        public string Description { get; set; }

        //Foreign Keys
        public long UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
