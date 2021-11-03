using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Subject.Commands.CreateSubject;
using TripleT.Application.Subject.Commands.DeleteSubject;
using TripleT.Application.Subject.Commands.UpdateSubject;
using TripleT.Application.Subject.Queries.SubjectList;

namespace TripleT.API.Controllers
{
    public class SubjectController : BaseController
    {
        public SubjectController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public Task<long> CreateAsync(CreateSubjectCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public Task UpdateAsync(UpdateSubjectCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{SubjectId}")]
        public Task DeleteAsync([FromRoute] DeleteSubjectCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpGet()]
        public Task<SubjectListQueryViewModel> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(new SubjectListQuery(), cancellationToken);
        }
    }
}
