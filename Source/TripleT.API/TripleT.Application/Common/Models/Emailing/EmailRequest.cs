using System.Collections.Generic;
using TripleT.Application.Common.Models.Emailing.Enumerations;

namespace TripleT.Application.Common.Models.Emailing
{
    public class EmailRequest<TTemplateData>
    {
        public EmailTypes EmailType { get; set; }
        public List<string> ToEmailAdresses { get; set; }
        public TTemplateData Data { get; set; }
    }
}
