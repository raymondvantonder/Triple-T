using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;
using TripleT.Domain.Exceptions;

namespace TripleT.Application.ModuleType.Commands.UpdateModuleType
{
    public class UpdateModuleTypeCommand : IRequest
    {
        public long ModuleTypeId { get; set; }
        public string NewName { get; set; }
    }

    public class UpdateModuleTypeCommandHandler : IRequestHandler<UpdateModuleTypeCommand>
    {
        private readonly ILogger<UpdateModuleTypeCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public UpdateModuleTypeCommandHandler(ILogger<UpdateModuleTypeCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(UpdateModuleTypeCommand request, CancellationToken cancellationToken)
        {
            ModuleTypeEntity moduleTypeEntity = await _contex.ModuleTypes.FindAsync(new object[] { request.ModuleTypeId }, cancellationToken);

            if (moduleTypeEntity == null)
            {
                _logger.LogError($"Could not find Module Type with id [{request.ModuleTypeId}]");
                throw new EntityNotFoundException($"Could not find Module Type with id [{request.ModuleTypeId}]");
            }

            if (moduleTypeEntity.TypeName == request.NewName)
            {
                _logger.LogWarning($"New name [{request.NewName}] and existing name [{moduleTypeEntity.TypeName}] is the same, not updating.");
                return Unit.Value;
            }

            moduleTypeEntity.TypeName = request.NewName;

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Module Type name updated successfully");

            return Unit.Value;
        }
    }
}
