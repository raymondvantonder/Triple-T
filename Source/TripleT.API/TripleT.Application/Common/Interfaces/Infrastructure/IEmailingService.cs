using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Models.Emailing;

namespace TripleT.Application.Interfaces.Infrastructure
{
    public interface IEmailingService
    {
        Task<string> SendEmailAsync<TTemplateData>(EmailRequest<TTemplateData> request, CancellationToken cancellationToken);
    }
}
