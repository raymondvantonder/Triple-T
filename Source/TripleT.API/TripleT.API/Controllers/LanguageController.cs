using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Language.Commands.CreateLanguage;
using TripleT.Application.Language.Commands.DeleteLanguage;
using TripleT.Application.Language.Commands.UpdateLanguage;
using TripleT.Application.Language.Queries.LanguageList;

namespace TripleT.API.Controllers
{
    public class LanguageController : BaseController
    {
        public LanguageController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public Task<long> CreateLanguageAsync(CreateLanguageCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public Task UpdateLanguageAsync(UpdateLanguageCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{LanguageId}")]
        public Task DeleteLanguageAsync([FromRoute] DeleteLanguageCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpGet()]
        public Task<LanguageListQueryViewModel> GetAllLanguagesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(new LanguageListQuery(), cancellationToken);
        }
    }
}
