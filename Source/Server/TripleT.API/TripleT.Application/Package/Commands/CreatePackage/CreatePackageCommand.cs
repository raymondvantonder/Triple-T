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

namespace TripleT.Application.Package.Commands.CreatePackage
{
    public class CreatePackageCommand : IRequest<long>
    {
        public string PackageName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<long> LinkedModules { get; set; }
        public long GradeId { get; set; }
        public long SubjectId { get; set; }
    }

    public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, long>
    {
        private readonly ILogger<CreatePackageCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public CreatePackageCommandHandler(ILogger<CreatePackageCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<long> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
        {
            GradeEntity gradeEntity = await _contex.Grades.MustFindAsync(request.GradeId, cancellationToken);
            SubjectEntity subjectEntity = await _contex.Subjects.MustFindAsync(request.SubjectId, cancellationToken);

            var packageEntity = new PackageEntity
            {
                Name = request.PackageName,
                Description = request.Description,
                Price = request.Price,
                Grade = gradeEntity,
                Subject = subjectEntity
            };

            var value = _contex.Modules.Where(x => request.LinkedModules.Contains(x.Id)).ToQueryString();

            if (request.LinkedModules?.Count() > 0)
            {
                packageEntity.Modules = await _contex.Modules.Where(x => request.LinkedModules.Contains(x.Id)).ToListAsync(cancellationToken);
            }

            await _contex.Packages.AddAsync(packageEntity, cancellationToken);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully created package");

            return packageEntity.Id;
        }
    }
}
