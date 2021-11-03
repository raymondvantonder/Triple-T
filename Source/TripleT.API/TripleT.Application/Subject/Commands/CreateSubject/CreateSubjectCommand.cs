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

namespace TripleT.Application.Subject.Commands.CreateSubject
{
    public class CreateSubjectCommand : IRequest<long>
    {
        public string Subject { get; set; }
    }

    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, long>
    {
        private readonly ILogger<CreateSubjectCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public CreateSubjectCommandHandler(ILogger<CreateSubjectCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<long> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            SubjectEntity existingSubjectEntity = await _contex.Subjects.FirstOrDefaultAsync(x => x.Name == request.Subject, cancellationToken);

            if (existingSubjectEntity != null)
            {
                _logger.LogError($"Subject [{request.Subject}] already exists");
                throw new DuplicateEntityException($"Subject [{request.Subject}] already exists");
            }

            var entity = new SubjectEntity { Name = request.Subject };

            await _contex.Subjects.AddAsync(entity, cancellationToken);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Added Subject [{request.Subject}] successfully");

            return entity.Id;
        }
    }
}
