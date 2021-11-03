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

namespace TripleT.Application.Subject.Commands.DeleteSubject
{
    public class DeleteSubjectCommand : IRequest
    {
        public long SubjectId { get; set; }
    }

    public class DeleteSummaryCategoryCommandHandler : IRequestHandler<DeleteSubjectCommand>
    {
        private readonly ILogger<DeleteSummaryCategoryCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public DeleteSummaryCategoryCommandHandler(ILogger<DeleteSummaryCategoryCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            SubjectEntity categoryEntity = await _contex.Subjects.FindAsync(new object[] { request.SubjectId }, cancellationToken);

            if (categoryEntity == null)
            {
                _logger.LogWarning($"Category [{request.SubjectId}] does not exist for deleting");
                return Unit.Value;
            }

            _contex.Subjects.Remove(categoryEntity);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Deleted category [{categoryEntity.FormatAsJsonForLogging()}]");
            
            return Unit.Value;
        }
    }
}
