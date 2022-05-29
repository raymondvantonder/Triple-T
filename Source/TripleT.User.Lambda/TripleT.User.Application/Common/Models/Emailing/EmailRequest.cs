using System.Collections.Generic;

namespace TripleT.User.Application.Common.Models.Emailing
{
    public class EmailRequest<TTemplateData>
    {
        public string Template { get; set; }
        public List<string> ToEmailAdresses { get; set; }
        public TTemplateData Data { get; set; }
    }
}
