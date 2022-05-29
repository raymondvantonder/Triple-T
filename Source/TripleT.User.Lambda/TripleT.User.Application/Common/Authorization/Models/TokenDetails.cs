namespace TripleT.User.Application.Common.Authorization.Models
{
    public class TokenDetails
    {
        public string Token { get; set; }
        public int ExpiresInSeconds { get; set; }
    }
}
