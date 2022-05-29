namespace TripleT.User.Application.Common.Models.Emailing
{
    public class ForgotPasswordEmailData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ResetPasswordUrl { get; set; }
    }
}
