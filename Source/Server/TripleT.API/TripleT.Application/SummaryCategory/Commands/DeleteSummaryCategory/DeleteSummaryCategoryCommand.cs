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

namespace TripleT.Application.SummaryCategory.Commands.DeleteSummaryCategory
{
    public class DeleteSummaryCategoryCommand : IRequest
    {
        public long CategoryId { get; set; }
    }

    public class DeleteSummaryCategoryCommandHandler : IRequestHandler<DeleteSummaryCategoryCommand>
    {
        private readonly ILogger<DeleteSummaryCategoryCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public DeleteSummaryCategoryCommandHandler(ILogger<DeleteSummaryCategoryCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(DeleteSummaryCategoryCommand request, CancellationToken cancellationToken)
        {
            SummaryCategoryEntity categoryEntity = await _contex.SummaryCategories.FindAsync(new object[] { request.CategoryId }, cancellationToken);

            if (categoryEntity == null)
            {
                _logger.LogWarning($"Category [{request.CategoryId}] does not exist for deleting");
                return Unit.Value;
            }

            _contex.SummaryCategories.Remove(categoryEntity);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Deleted category [{categoryEntity.FormatAsJsonForLogging()}]");
            
            return Unit.Value;
        }
    }
}
