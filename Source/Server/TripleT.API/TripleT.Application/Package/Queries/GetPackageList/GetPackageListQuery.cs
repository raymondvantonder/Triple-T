using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace TripleT.Application.Package.Queries.GetPackageList
{
    public class GetPackageListQuery : IRequest<GetPackageListQueryViewModel>
    {
        public long? SubjectId { get; set; }
        public long? GradeId { get; set; }
    }

    public class GetPackageListQueryHandler : IRequestHandler<GetPackageListQuery, GetPackageListQueryViewModel>
    {
        private readonly ILogger<GetPackageListQueryHandler> _logger;
        private readonly ITripleTDbContext _contex;
        private readonly IMapper _mapper;

        public GetPackageListQueryHandler(ILogger<GetPackageListQueryHandler> logger, ITripleTDbContext context, IMapper mapper)
        {
            _logger = logger;
            _contex = context;
            _mapper = mapper;
        }

        public async Task<GetPackageListQueryViewModel> Handle(GetPackageListQuery request, CancellationToken cancellationToken)
        {
            var packagesQuery = _contex.Packages.AsQueryable();

            if (request.SubjectId > 0)
                packagesQuery = packagesQuery.Where(x => x.SubjectId == request.SubjectId);

            if (request.GradeId > 0)
                packagesQuery = packagesQuery.Where(x => x.GradeId == request.GradeId);

            var packages = await packagesQuery.ProjectTo<GetPackageListQueryDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            _logger.LogInformation($"Found {packages?.Count} packages");

            return new GetPackageListQueryViewModel { Packages = packages };
        }
    }
}
