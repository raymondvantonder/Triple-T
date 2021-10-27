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

namespace TripleT.Application.Summary.Commands.UpdateSummary
{
    public class UpdateSummaryCommand : IRequest
    {
        public long SummaryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string FileLocation { get; set; }
        public long CategoryId { get; set; }
    }

    public class UpdatedSummaryCommandHandler : IRequestHandler<UpdateSummaryCommand>
    {
        private readonly ILogger<UpdatedSummaryCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public UpdatedSummaryCommandHandler(ILogger<UpdatedSummaryCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(UpdateSummaryCommand request, CancellationToken cancellationToken)
        {
            SummaryEntity summaryEntity = await _contex.Summaries.FindAsync(new object[] { request.SummaryId }, cancellationToken);

            if (summaryEntity == null)
            {
                _logger.LogError($"Could not find summary with id [{request.SummaryId}]");
                throw new EntityNotFoundException($"Could not find summary with id [{request.SummaryId}]");
            }

            if (summaryEntity.SummaryCategoryId != request.CategoryId)
            {
                SummaryCategoryEntity newCategory = await _contex.SummaryCategories.FindAsync(new object[] { request.CategoryId }, cancellationToken);

                if (newCategory == null)
                {
                    _logger.LogError($"Could not find Summary Category with id [{request.CategoryId}]");
                    throw new EntityNotFoundException($"Could not find Summary Category with id [{request.CategoryId}]");
                }

                summaryEntity.SummaryCategory = newCategory;
            }

            summaryEntity.Name = request.Name;
            summaryEntity.Description = request.Description;
            summaryEntity.Price = request.Price;
            summaryEntity.FileLocation = request.FileLocation;

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Updated summary successfully");

            return Unit.Value;
        }

        private bool MatchValues(string value1, string value2)
        {
            if (value1 == value2)
            {
                return true;
            }

            return false;
        }
    }
}
