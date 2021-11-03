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

namespace TripleT.Application.ModuleType.Queries.GetModuleTypesList
{
    public class GetModuleTypesListQuery : IRequest<GetModuleTypesListQueryViewModel>
    {
    }

    public class GetModuleTypesListQueryHandler : IRequestHandler<GetModuleTypesListQuery, GetModuleTypesListQueryViewModel>
    {
        private readonly ILogger<GetModuleTypesListQueryHandler> _logger;
        private readonly ITripleTDbContext _contex;
        private readonly IMapper _mapper;

        public GetModuleTypesListQueryHandler(ILogger<GetModuleTypesListQueryHandler> logger, ITripleTDbContext context, IMapper mapper)
        {
            _logger = logger;
            _contex = context;
            _mapper = mapper;
        }

        public async Task<GetModuleTypesListQueryViewModel> Handle(GetModuleTypesListQuery request, CancellationToken cancellationToken)
        {
            var moduleTypes = await _contex.ModuleTypes.ProjectTo<GetModuleTypesListQueryDto>(_mapper.ConfigurationProvider).ToListAsync();

            _logger.LogInformation($"Found [{moduleTypes?.Count}] module types");

            return new GetModuleTypesListQueryViewModel { ModuleTypes = moduleTypes };
        }
    }
}
