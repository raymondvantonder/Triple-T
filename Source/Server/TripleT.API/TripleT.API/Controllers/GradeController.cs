using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Grade.Commands.CreateGrade;
using TripleT.Application.Grade.Commands.DeleteGrade;
using TripleT.Application.Grade.Commands.UpdateGrade;
using TripleT.Application.Grade.Queries.GradeList;

namespace TripleT.API.Controllers
{
    public class GradeController : BaseController
    {
        public GradeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public Task<long> CreateAsync(CreateGradeCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public Task UpdateAsync(UpdateGradeCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{GradeId}")]
        public Task DeleteAsync([FromRoute] DeleteGradeCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpGet()]
        public Task<GradeListQueryViewModel> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(new GradeListQuery(), cancellationToken);
        }
    }
}
