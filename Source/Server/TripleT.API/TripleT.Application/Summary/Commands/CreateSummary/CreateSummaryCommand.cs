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
using TripleT.Domain.Exceptions;

namespace TripleT.Application.Summary.Commands.CreateSummary
{
    public class CreateSummaryCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string FileLocation { get; set; }
        public long CategoryId { get; set; }
    }

    public class CreateSummaryCommandHandler : IRequestHandler<CreateSummaryCommand, long>
    {
        private readonly ILogger<CreateSummaryCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public CreateSummaryCommandHandler(ILogger<CreateSummaryCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<long> Handle(CreateSummaryCommand request, CancellationToken cancellationToken)
        {
            SummaryEntity existingSummary = await _contex.Summaries.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (existingSummary != null)
            {
                _logger.LogError($"Summary with name [{request.Name}] already exists.");
                throw new DuplicateEntityException($"Summary with name [{request.Name}] already exists.");
            }

            SummaryCategoryEntity existingCategory = await _contex.SummaryCategories.FindAsync(new object[] { request.CategoryId }, cancellationToken);

            if (existingCategory == null)
            {
                _logger.LogError($"Could not find category with id [{request.CategoryId}]");
                throw new EntityNotFoundException($"Could not find category with id [{request.CategoryId}]");
            }

            var summaryEntity = new SummaryEntity
            {
                FileLocation = request.FileLocation,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                SummaryCategory = existingCategory
            };

            //Check if file exists in s3 bucket

            await _contex.Summaries.AddAsync(summaryEntity, cancellationToken);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Successfully created summary with id [{summaryEntity.Id}].");

            return summaryEntity.Id;
        }
    }
}
