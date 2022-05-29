using System;
using TripleT.User.Domain.Primitives;

namespace TripleT.User.Infrastructure.Persistence.Models
{
    public class UserDocument : DocumentBase<UserDocument>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserRoles Role { get; set; }
        public EmailVerification EmailVerification { get; set; }
    }

    public class EmailVerification
    {
        public string Reference { get; set; }
        public bool Verified { get; set; }
        public DateTime? VerifiedTime { get; set; }
    }
}