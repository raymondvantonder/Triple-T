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

namespace TripleT.Application.Summary.Commands.DeleteSummary
{
    public class DeleteSummaryCommand : IRequest
    {
        public long SummaryId { get; set; }
    }

    public class DeleteSummaryCommandHandler : IRequestHandler<DeleteSummaryCommand>
    {
        private readonly ILogger<DeleteSummaryCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public DeleteSummaryCommandHandler(ILogger<DeleteSummaryCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(DeleteSummaryCommand request, CancellationToken cancellationToken)
        {
            SummaryEntity summaryEntity = await _contex.Summaries.FindAsync(new object[] { request.SummaryId }, cancellationToken);

            if (summaryEntity == null)
            {
                _logger.LogWarning($"Summary [{request.SummaryId}] does not exist for deleting.");
                return Unit.Value;
            }

            _contex.Summaries.Remove(summaryEntity);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Deleted category [{summaryEntity.FormatAsJsonForLogging()}]");

            return Unit.Value;
        }
    }
}
