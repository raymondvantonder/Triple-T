using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Package.Commands.CreatePackage;
using TripleT.Application.Package.Commands.DeletePackage;
using TripleT.Application.Package.Commands.UpdatePackage;
using TripleT.Application.Package.Queries.GetPackageList;

namespace TripleT.API.Controllers
{
    public class PackageController : BaseController
    {
        public PackageController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public Task<long> CreateAsync(CreatePackageCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public Task UpdateAsync(UpdatePackageCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{PackageId}")]
        public Task DeleteAsync([FromRoute] DeletePackageCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(command, cancellationToken);
        }

        [HttpGet()]
        public Task<GetPackageListQueryViewModel> GetAllAsync([FromQuery] GetPackageListQuery query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(query, cancellationToken);
        }
    }
}
