using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Module.Commands.CreateModule;
using TripleT.Application.Module.Commands.DeleteModule;
using TripleT.Application.Module.Commands.UpdateModule;
using TripleT.Application.Module.Queries.ModulesList;
using TripleT.Application.Package.Queries.GetPackageList;

namespace TripleT.API.Controllers
{
    public class ModuleController : BaseController
    {
        public ModuleController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public Task<long> CreateAsync([FromBody] CreateModuleCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public Task UpdateAsync([FromBody] UpdateModuleCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{ModuleId}")]
        public Task DeleteAsync([FromRoute] DeleteModuleCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command, cancellationToken);
        }

        [HttpGet]
        public Task<ModulesListQueryViewModel> GetAllAsync([FromQuery] ModulesListQuery query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(query, cancellationToken);
        }

        //[HttpGet("packages")]
        //public Task<GetPackageListQueryViewModel> GetAllPackagesAsync(CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    return Mediator.Send(new GetPackageListQuery(), cancellationToken);
        //}

    }
}
