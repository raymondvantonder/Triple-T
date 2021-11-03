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

namespace TripleT.Application.Subject.Commands.UpdateSubject
{
    public class UpdateSubjectCommand : IRequest
    {
        public long SubjectId { get; set; }
        public string NewName { get; set; }
    }

    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand>
    {
        private readonly ILogger<UpdateSubjectCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public UpdateSubjectCommandHandler(ILogger<UpdateSubjectCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            SubjectEntity subjectEntity = await _contex.Subjects.MustFindAsync(request.SubjectId, cancellationToken);

            if (subjectEntity.Name == request.NewName)
            {
                _logger.LogWarning($"New name [{request.NewName}] and existing name [{subjectEntity.Name}] is the same, not updating.");
                return Unit.Value;
            }

            subjectEntity.Name = request.NewName;

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Subject name updated successfully");

            return Unit.Value;
        }
    }
}
