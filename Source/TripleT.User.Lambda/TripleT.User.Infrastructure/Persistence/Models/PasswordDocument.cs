using System;

namespace TripleT.User.Infrastructure.Persistence.Models
{
    public class PasswordDocument : DocumentBase<PasswordDocument>
    {
        public string Email { get; set; }
        public string Value { get; set; }
        public string Salt { get; set; }
        public PasswordResetDetails PasswordResetDetails { get; set; }
    }

    public class PasswordResetDetails
    {
        public string Reference { get; set; }
        
        public DateTime ExpiryTime { get; set; }
    }
}