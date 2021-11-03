using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Extensions;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;
using TripleT.Domain.Exceptions;

namespace TripleT.Application.Grade.Commands.CreateGrade
{
    public class CreateGradeCommand : IRequest<long>
    {
        public string Grade { get; set; }
    }

    public class CreateGradeCommandHandler : IRequestHandler<CreateGradeCommand, long>
    {
        private readonly ILogger<CreateGradeCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public CreateGradeCommandHandler(ILogger<CreateGradeCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<long> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            GradeEntity existingGradeEntity = await _contex.Grades.FirstOrDefaultAsync(x => x.Value == request.Grade, cancellationToken);

            if (existingGradeEntity != null)
            {
                _logger.LogError($"Grade [{request.Grade}] already exists");
                throw new DuplicateEntityException($"Grade [{request.Grade}] already exists");
            }

            var entity = new GradeEntity { Value = request.Grade };

            await _contex.Grades.AddAsync(entity, cancellationToken);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Created grade [{entity.FormatAsJsonForLogging()}] successfully");

            return entity.Id;
        }
    }
}
