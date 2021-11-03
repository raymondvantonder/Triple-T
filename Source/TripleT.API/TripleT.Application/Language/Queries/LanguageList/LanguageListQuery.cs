using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;

namespace TripleT.Application.Language.Queries.LanguageList
{
    public class LanguageListQuery : IRequest<LanguageListQueryViewModel>
    {
    }

    public class LanguageListQueryHandler : IRequestHandler<LanguageListQuery, LanguageListQueryViewModel>
    {
        private readonly ILogger<LanguageListQueryHandler> _logger;
        private readonly ITripleTDbContext _contex;
        private readonly IMapper _mapper;

        public LanguageListQueryHandler(ILogger<LanguageListQueryHandler> logger, ITripleTDbContext context, IMapper mapper)
        {
            _logger = logger;
            _contex = context;
            _mapper = mapper;
        }

        public async Task<LanguageListQueryViewModel> Handle(LanguageListQuery request, CancellationToken cancellationToken)
        {
            List<LanguageListQueryDto> languages = await _contex.Languages.ProjectTo<LanguageListQueryDto>(_mapper.ConfigurationProvider).ToListAsync();

            _logger.LogInformation($"Found [{languages?.Count}] languages");

            return new LanguageListQueryViewModel { Languages = languages };
        }
    }
}
