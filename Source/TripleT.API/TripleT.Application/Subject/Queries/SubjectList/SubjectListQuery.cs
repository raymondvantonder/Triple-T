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

namespace TripleT.Application.Subject.Queries.SubjectList
{
    public class SubjectListQuery : IRequest<SubjectListQueryViewModel>
    {

    }

    public class SubjectListQueryHandler : IRequestHandler<SubjectListQuery, SubjectListQueryViewModel>
    {
        private readonly ILogger<SubjectListQueryHandler> _logger;
        private readonly ITripleTDbContext _contex;
        private readonly IMapper _mapper;

        public SubjectListQueryHandler(ILogger<SubjectListQueryHandler> logger, ITripleTDbContext context, IMapper mapper)
        {
            _logger = logger;
            _contex = context;
            _mapper = mapper;
        }

        public async Task<SubjectListQueryViewModel> Handle(SubjectListQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _contex.Subjects.ProjectTo<SubjectListQueryDto>(_mapper.ConfigurationProvider).ToListAsync();

            _logger.LogInformation($"Found [{subjects?.Count}] categories");

            return new SubjectListQueryViewModel { Subjects = subjects };
        }
    }
}
