namespace TripleT.User.Application.Common.Models.Configuration
{
    public class MailSettings
    {
        public bool Enabled { get; set; }
        public string VerificationUri { get; set; }
        public string PasswordResetUri { get; set; }
    }
}