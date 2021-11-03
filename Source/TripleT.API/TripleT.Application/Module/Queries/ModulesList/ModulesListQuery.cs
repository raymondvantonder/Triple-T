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

namespace TripleT.Application.Module.Queries.ModulesList
{
    public class ModulesListQuery : IRequest<ModulesListQueryViewModel>
    {
        public long? SubjectId { get; set; }
        public long? GradeId { get; set; }
        public long? ModuleTypeId { get; set; }
        public long? LanguageId { get; set; }
    }

    public class ModulesListQueryHandler : IRequestHandler<ModulesListQuery, ModulesListQueryViewModel>
    {
        private readonly ILogger<ModulesListQueryHandler> _logger;
        private readonly ITripleTDbContext _contex;
        private readonly IMapper _mapper;

        public ModulesListQueryHandler(ILogger<ModulesListQueryHandler> logger, ITripleTDbContext context, IMapper mapper)
        {
            _logger = logger;
            _contex = context;
            _mapper = mapper;
        }

        public async Task<ModulesListQueryViewModel> Handle(ModulesListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ModuleEntity> modulesQuery = _contex.Modules.AsQueryable();

            if (request.SubjectId > 0)
                modulesQuery = modulesQuery.Where(x => x.SubjectId == request.SubjectId);

            if (request.GradeId > 0)
                modulesQuery = modulesQuery.Where(x => x.Grade.Id == request.GradeId);

            if (request.ModuleTypeId > 0)
                modulesQuery = modulesQuery.Where(x => x.ModuleTypeId == request.ModuleTypeId);

            if (request.LanguageId > 0)
                modulesQuery = modulesQuery.Where(x => x.LanguageId == request.LanguageId);

            List<ModulesListQueryDto> modules = await modulesQuery
                                                                .Include(x => x.Subject)
                                                                .Include(x => x.Grade)
                                                                .Include(x => x.Language)
                                                                .Include(x => x.ModuleType)
                                                                .ProjectTo<ModulesListQueryDto>(_mapper.ConfigurationProvider).ToListAsync();

            _logger.LogInformation($"Found [{modules?.Count}] summaries");

            return new ModulesListQueryViewModel { Modules = modules };
        }
    }
}
