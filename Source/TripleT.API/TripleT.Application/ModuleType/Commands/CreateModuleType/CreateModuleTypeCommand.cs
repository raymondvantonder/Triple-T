using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;
using TripleT.Domain.Exceptions;

namespace TripleT.Application.ModuleType.Commands.CreateModuleType
{
    public class CreateModuleTypeCommand : IRequest<long>
    {
        public string Name { get; set; }
    }

    public class CreateModuleTypeCommandHandler : IRequestHandler<CreateModuleTypeCommand, long>
    {
        private readonly ILogger<CreateModuleTypeCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public CreateModuleTypeCommandHandler(ILogger<CreateModuleTypeCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<long> Handle(CreateModuleTypeCommand request, CancellationToken cancellationToken)
        {
            var existingModuleTypeEntity = await _contex.ModuleTypes.FirstOrDefaultAsync(x => x.TypeName == request.Name, cancellationToken);

            if (existingModuleTypeEntity != null)
            {
                _logger.LogError($"Module Type [{request.Name}] already exists");
                throw new DuplicateEntityException($"Module Type [{request.Name}] already exists");
            }

            var entity = new ModuleTypeEntity { TypeName = request.Name };

            await _contex.ModuleTypes.AddAsync(entity, cancellationToken);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Added Module Type [{request.Name}] successfully");

            return entity.Id;
        }
    }
}
