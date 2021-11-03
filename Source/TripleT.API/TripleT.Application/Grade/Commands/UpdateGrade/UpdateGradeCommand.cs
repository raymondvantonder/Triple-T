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

namespace TripleT.Application.Grade.Commands.UpdateGrade
{
    public class UpdateGradeCommand : IRequest
    {
        public long GradeId { get; set; }
        public string NewGrade { get; set; }
    }

    public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeCommand>
    {
        private readonly ILogger<UpdateGradeCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public UpdateGradeCommandHandler(ILogger<UpdateGradeCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            GradeEntity gradeEntity = await _contex.Grades.FindAsync(new object[] { request.GradeId }, cancellationToken);

            if (gradeEntity == null)
            {
                _logger.LogError($"Grade [{request.GradeId}] does not exist");
                throw new EntityNotFoundException($"Grade [{request.GradeId}] does not exist");
            }

            gradeEntity.Value = request.NewGrade;

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogError($"Grade updated successfully: [{gradeEntity}]");

            return Unit.Value;
        }
    }
}
