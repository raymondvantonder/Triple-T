using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Extensions;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;

namespace TripleT.Application.Module.Commands.DeleteModule
{
    public class DeleteModuleCommand : IRequest
    {
        public long ModuleId { get; set; }
    }

    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand>
    {
        private readonly ILogger<DeleteModuleCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public DeleteModuleCommandHandler(ILogger<DeleteModuleCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            ModuleEntity summaryEntity = await _contex.Modules.FindAsync(new object[] { request.ModuleId }, cancellationToken);

            if (summaryEntity == null)
            {
                _logger.LogWarning($"Summary [{request.ModuleId}] does not exist for deleting.");
                return Unit.Value;
            }

            _contex.Modules.Remove(summaryEntity);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Deleted category [{summaryEntity.FormatAsJsonForLogging()}]");

            return Unit.Value;
        }
    }
}
