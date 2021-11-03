using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class UserEntity : AuditableEntity
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //Foreign Keys
        public long RoleId { get; set; }
        public long? EmailVerificationId { get; set; }
        public long? PasswordResetDetailId { get; set; }


        public EmailVerificationEntity EmailVerification { get; set; }
        public RoleEntity Role { get; set; }
        public PasswordResetDetailEntity PasswordResetDetail { get; set; }
        public IList<UserAuditEntity> UserAudits { get; set; } = new List<UserAuditEntity>();
    }
}
