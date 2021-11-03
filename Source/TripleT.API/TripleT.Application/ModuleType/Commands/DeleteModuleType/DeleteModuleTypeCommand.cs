using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Extensions;
using TripleT.Application.Common.Interfaces.Infrastructure;

namespace TripleT.Application.ModuleType.Commands.DeleteModuleType
{
    public class DeleteModuleTypeCommand : IRequest
    {
        public long ModuleTypeId { get; set; }
    }

    public class DeleteModuleTypeCommandHandler : IRequestHandler<DeleteModuleTypeCommand>
    {
        private readonly ILogger<DeleteModuleTypeCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public DeleteModuleTypeCommandHandler(ILogger<DeleteModuleTypeCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(DeleteModuleTypeCommand request, CancellationToken cancellationToken)
        {
            var moduleTypeEntity = await _contex.ModuleTypes.MustFindAsync(request.ModuleTypeId, cancellationToken);

            _contex.ModuleTypes.Remove(moduleTypeEntity);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Deleted Module Type [{moduleTypeEntity.FormatAsJsonForLogging()}]");

            return Unit.Value;
        }
    }
}
