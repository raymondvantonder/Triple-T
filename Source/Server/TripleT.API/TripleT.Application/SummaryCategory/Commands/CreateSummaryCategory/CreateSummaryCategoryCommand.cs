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

namespace TripleT.Application.SummaryCategory.Commands.CreateSummaryCategory
{
    public class CreateSummaryCategoryCommand : IRequest<long>
    {
        public string Name { get; set; }
    }

    public class CreateSummaryCategoryCommandHandler : IRequestHandler<CreateSummaryCategoryCommand, long>
    {
        private readonly ILogger<CreateSummaryCategoryCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public CreateSummaryCategoryCommandHandler(ILogger<CreateSummaryCategoryCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<long> Handle(CreateSummaryCategoryCommand request, CancellationToken cancellationToken)
        {
            SummaryCategoryEntity existingCategoryEntity = await _contex.SummaryCategories.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (existingCategoryEntity != null)
            {
                _logger.LogError($"Category [{request.Name}] already exists");
                throw new DuplicateEntityException($"Category [{request.Name}] already exists");
            }

            var entity = new SummaryCategoryEntity { Name = request.Name };

            await _contex.SummaryCategories.AddAsync(entity, cancellationToken);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Added Category [{request.Name}] successfully");

            return entity.Id;
        }
    }
}
