using System;

namespace TripleT.User.Domain.Domain
{
    public class PasswordEntity : EntityBase
    {
        public PasswordEntity()
        {
            
        }
        
        public PasswordEntity(string salt, string value)
        {
            Salt = salt;
            Value = value;
        }

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