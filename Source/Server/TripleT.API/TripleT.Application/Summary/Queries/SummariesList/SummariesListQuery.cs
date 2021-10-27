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

namespace TripleT.Application.Summary.Queries.SummariesList
{
    public class SummariesListQuery : IRequest<SummariesListQueryViewModel>
    {
        public long? CategoryId { get; set; }
    }

    public class SummariesListQueryHandler : IRequestHandler<SummariesListQuery, SummariesListQueryViewModel>
    {
        private readonly ILogger<SummariesListQueryHandler> _logger;
        private readonly ITripleTDbContext _contex;
        private readonly IMapper _mapper;

        public SummariesListQueryHandler(ILogger<SummariesListQueryHandler> logger, ITripleTDbContext context, IMapper mapper)
        {
            _logger = logger;
            _contex = context;
            _mapper = mapper;
        }

        public async Task<SummariesListQueryViewModel> Handle(SummariesListQuery request, CancellationToken cancellationToken)
        {
            List<SummaryPackageEntity> result = await _contex.SummaryPackages
                                                                    .Include(x => x.SummaryCategory)
                                                                    .Include(x => x.PackageSummaryLinks)
                                                                    .Where(x => x.SummaryCategory.Name == "Maths")
                                                                    .ToListAsync();



            IQueryable<SummaryEntity> summariesQuery = _contex.Summaries.AsQueryable();

            if (request.CategoryId.HasValue && request.CategoryId > 0)
            {
                summariesQuery = summariesQuery.Where(x => x.SummaryCategoryId == request.CategoryId);
            }

            List<SummariesListQueryDto> summaries = await summariesQuery.Include(x => x.SummaryCategory).ProjectTo<SummariesListQueryDto>(_mapper.ConfigurationProvider).ToListAsync();

            _logger.LogInformation($"Found [{summaries?.Count}] summaries");

            return new SummariesListQueryViewModel { Summaries = summaries };
        }
    }
}
