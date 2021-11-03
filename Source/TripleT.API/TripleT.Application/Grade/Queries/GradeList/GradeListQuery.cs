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

namespace TripleT.Application.Grade.Queries.GradeList
{
    public class GradeListQuery : IRequest<GradeListQueryViewModel>
    {
    }

    public class GradeListQueryHandler : IRequestHandler<GradeListQuery, GradeListQueryViewModel>
    {
        private readonly ILogger<GradeListQueryHandler> _logger;
        private readonly ITripleTDbContext _contex;
        private readonly IMapper _mapper;

        public GradeListQueryHandler(ILogger<GradeListQueryHandler> logger, ITripleTDbContext context, IMapper mapper)
        {
            _logger = logger;
            _contex = context;
            _mapper = mapper;
        }

        public async Task<GradeListQueryViewModel> Handle(GradeListQuery request, CancellationToken cancellationToken)
        {
            List<GradeListQueryDto> grades = await _contex.Grades.ProjectTo<GradeListQueryDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            _logger.LogInformation($"Found [{grades?.Count}] grades.");

            return new GradeListQueryViewModel { Grades = grades };
        }
    }
}
