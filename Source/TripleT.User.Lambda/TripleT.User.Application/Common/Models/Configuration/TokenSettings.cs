namespace TripleT.User.Application.Common.Models.Configuration
{
    public class TokenSettings
    {
        public string Secret { get; set; }
        public int ExpiresInSeconds { get; set; }
    }
}
