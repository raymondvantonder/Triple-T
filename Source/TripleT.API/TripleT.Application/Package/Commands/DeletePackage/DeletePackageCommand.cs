using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Extensions;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;

namespace TripleT.Application.Package.Commands.DeletePackage
{
    public class DeletePackageCommand : IRequest
    {
        public long PackageId { get; set; }
    }

    public class DeletePackageCommandHandler : IRequestHandler<DeletePackageCommand>
    {
        private readonly ILogger<DeletePackageCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public DeletePackageCommandHandler(ILogger<DeletePackageCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(DeletePackageCommand request, CancellationToken cancellationToken)
        {
            PackageEntity packageEntity = await _contex.Packages.FindAsync(new object[] { request.PackageId }, cancellationToken);

            if (packageEntity == null)
            {
                _logger.LogWarning($"Package [{request.PackageId}] not found for deleting.");
                return Unit.Value;
            }

            _contex.Packages.Remove(packageEntity);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Package deleted: [{packageEntity.FormatAsJsonForLogging()}]");

            return Unit.Value;
        }
    }
}
