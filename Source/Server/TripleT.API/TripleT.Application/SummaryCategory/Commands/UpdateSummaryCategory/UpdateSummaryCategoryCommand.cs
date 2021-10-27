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

namespace TripleT.Application.SummaryCategory.Commands.UpdateSummaryCategory
{
    public class UpdateSummaryCategoryCommand : IRequest
    {
        public long CategoryId { get; set; }
        public string NewName { get; set; }
    }

    public class UpdateSummaryCategoryCommandHandler : IRequestHandler<UpdateSummaryCategoryCommand>
    {
        private readonly ILogger<UpdateSummaryCategoryCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public UpdateSummaryCategoryCommandHandler(ILogger<UpdateSummaryCategoryCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(UpdateSummaryCategoryCommand request, CancellationToken cancellationToken)
        {
            SummaryCategoryEntity categoryEntity = await _contex.SummaryCategories.FindAsync(new object[] { request.CategoryId }, cancellationToken);

            if (categoryEntity == null)
            {
                _logger.LogError($"Could not find category with id [{request.CategoryId}]");
                throw new EntityNotFoundException($"Could not find category with id [{request.CategoryId}]");
            }

            if (categoryEntity.Name == request.NewName)
            {
                _logger.LogWarning($"New name [{request.NewName}] and existing name [{categoryEntity.Name}] is the same, not updating.");
                return Unit.Value;
            }

            categoryEntity.Name = request.NewName;

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Category name updated successfully");

            return Unit.Value;
        }
    }
}
