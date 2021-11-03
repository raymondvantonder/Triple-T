using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.ModuleType.Commands.CreateModuleType;
using TripleT.Application.ModuleType.Commands.DeleteModuleType;
using TripleT.Application.ModuleType.Commands.UpdateModuleType;
using TripleT.Application.ModuleType.Queries.GetModuleTypesList;

namespace TripleT.API.Controllers
{
    public class ModuleTypeController : BaseController
    {
        public ModuleTypeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public Task<long> CreateAsync(CreateModuleTypeCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public Task UpdateAsync(UpdateModuleTypeCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{ModuleTypeId}")]
        public Task DeleteAsync([FromRoute] DeleteModuleTypeCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpGet()]
        public Task<GetModuleTypesListQueryViewModel> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(new GetModuleTypesListQuery(), cancellationToken);
        }
    }
}
