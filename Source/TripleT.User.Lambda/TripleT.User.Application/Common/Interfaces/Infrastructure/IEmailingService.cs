using System.Threading;
using System.Threading.Tasks;
using TripleT.User.Application.Common.Models.Emailing;

namespace TripleT.User.Application.Common.Interfaces.Infrastructure
{
    public interface IEmailingService
    {
        Task<string> SendEmailAsync<TTemplateData>(EmailRequest<TTemplateData> request, CancellationToken cancellationToken);
    }
}
