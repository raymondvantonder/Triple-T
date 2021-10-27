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

namespace TripleT.Application.SummaryCategory.Queries.SummaryCategoryList
{
    public class SummaryCategoryListQuery : IRequest<SummaryCategoryListQueryViewModel>
    {

    }

    public class SummaryCategoryListQueryHandler : IRequestHandler<SummaryCategoryListQuery, SummaryCategoryListQueryViewModel>
    {
        private readonly ILogger<SummaryCategoryListQueryHandler> _logger;
        private readonly ITripleTDbContext _contex;
        private readonly IMapper _mapper;

        public SummaryCategoryListQueryHandler(ILogger<SummaryCategoryListQueryHandler> logger, ITripleTDbContext context, IMapper mapper)
        {
            _logger = logger;
            _contex = context;
            _mapper = mapper;
        }

        public async Task<SummaryCategoryListQueryViewModel> Handle(SummaryCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _contex.SummaryCategories.ProjectTo<SummaryCategoryListQueryDto>(_mapper.ConfigurationProvider).ToListAsync();

            _logger.LogInformation($"Found [{categories?.Count}] categories");

            return new SummaryCategoryListQueryViewModel { SummaryCategories = categories };
        }
    }
}
