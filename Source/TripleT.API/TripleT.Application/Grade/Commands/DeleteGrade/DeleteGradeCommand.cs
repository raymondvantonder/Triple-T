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

namespace TripleT.Application.Grade.Commands.DeleteGrade
{
    public class DeleteGradeCommand : IRequest
    {
        public long GradeId { get; set; }
    }

    public class DeleteGradeCommandHandler : IRequestHandler<DeleteGradeCommand>
    {
        private readonly ILogger<DeleteGradeCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public DeleteGradeCommandHandler(ILogger<DeleteGradeCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
        {
            GradeEntity gradeEntity = await _contex.Grades.FindAsync(new object[] { request.GradeId }, cancellationToken);

            if (gradeEntity == null)
            {
                _logger.LogWarning($"Grade [{request.GradeId}] not found for deleting.");
                return Unit.Value;
            }

            _contex.Grades.Remove(gradeEntity);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Grade deleted: [{gradeEntity.FormatAsJsonForLogging()}]");

            return Unit.Value;
        }
    }
}
