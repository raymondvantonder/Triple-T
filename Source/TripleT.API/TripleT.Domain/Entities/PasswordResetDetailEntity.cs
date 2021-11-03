using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class PasswordResetDetailEntity : AuditableEntity
    {
        public string Reference { get; set; }
        public DateTime ExpiryTime { get; set; }

        //Foreign Keys
        public long UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
