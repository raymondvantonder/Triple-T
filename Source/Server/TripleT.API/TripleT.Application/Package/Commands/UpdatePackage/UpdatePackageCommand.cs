using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;
using TripleT.Domain.Exceptions;

namespace TripleT.Application.Package.Commands.UpdatePackage
{
    public class UpdatePackageCommand : IRequest
    {
        public long PackageId { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public IEnumerable<long> LinkedModules { get; set; }
        public long? GradeId { get; set; }
        public long? SubjectId { get; set; }
    }

    public class UpdatePackageCommandHandler : IRequestHandler<UpdatePackageCommand>
    {
        private readonly ILogger<UpdatePackageCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public UpdatePackageCommandHandler(ILogger<UpdatePackageCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
        {
            PackageEntity existingPackageEntity = await _contex.Packages
                                                                    .Include(x => x.Grade)
                                                                    .Include(x => x.Subject)
                                                                    .Include(x => x.Modules)
                                                                    .FirstOrDefaultAsync(x => x.Id == request.PackageId, cancellationToken);

            if (existingPackageEntity == null)
            {
                throw new EntityNotFoundException($"Failed to update package, could not find package with id [{request.PackageId}]");
            }

            existingPackageEntity.GradeId = request.GradeId ?? existingPackageEntity.GradeId;
            existingPackageEntity.SubjectId = request.SubjectId ?? existingPackageEntity.SubjectId;

            existingPackageEntity.Name = request.PackageName ?? existingPackageEntity.Name;
            existingPackageEntity.Description = request.Description ?? existingPackageEntity.Description;
            existingPackageEntity.Price = request.Price ?? existingPackageEntity.Price;

            var modulesToRemove = existingPackageEntity.Modules.Select(x => x.Id).Except(request.LinkedModules);
            var modulesToAdd = request.LinkedModules.Except(existingPackageEntity.Modules.Select(x => x.Id));

            if (modulesToRemove?.Count() > 0)
            {
                _logger.LogInformation($"Removing [{modulesToAdd?.Count()}] modules from package [{request.PackageName}]");

                foreach (var module in existingPackageEntity.Modules.Where(x => modulesToRemove.Contains(x.Id)).ToList())
                {
                    existingPackageEntity.Modules.Remove(module);
                }
            }

            if (modulesToAdd?.Count() > 0)
            {
                _logger.LogInformation($"Adding [{modulesToAdd?.Count()}] modules to package [{request.PackageName}]");

                List<ModuleEntity> newModules = await _contex.Modules.Where(x => modulesToAdd.Contains(x.Id)).ToListAsync(cancellationToken);

                foreach (var module in newModules)
                {
                    existingPackageEntity.Modules.Add(module);
                }
            }

            await _contex.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
