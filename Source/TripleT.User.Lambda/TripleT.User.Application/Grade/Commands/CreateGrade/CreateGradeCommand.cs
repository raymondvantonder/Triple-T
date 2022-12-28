using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Grade.Commands.CreateGrade
{
    public class CreateGradeCommand : IRequest 
    {
        public string Name { get; set; }
    }

    public class CreateGradeCommandHandler : IRequestHandler<CreateGradeCommand>
    {
        private readonly ILogger<CreateGradeCommandHandler> _logger;
        private readonly IGradeRepository _gradeRepository;

        public CreateGradeCommandHandler(ILogger<CreateGradeCommandHandler> logger,
            IGradeRepository gradeRepository)
        {
            _logger = logger;
            _gradeRepository = gradeRepository;
        }
    
        public async Task<Unit> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            var existingGrades = await _gradeRepository.GetAllAsync(cancellationToken);

            if (existingGrades != null && existingGrades.Any(x => x.Name.Equals(request.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                _logger.LogWarning($"Cannot create grade, grade [{request.Name}] already exists");
            
                throw new InvalidOperationException($"Cannot create grade, grade [{request.Name}] already exists");
            }
        
            var gradeEntity = new GradeEntity
            {
                Id = Guid.NewGuid().ToString(), Name = request.Name
            };

            await _gradeRepository.AddItemAsync(gradeEntity, cancellationToken);
        
            return Unit.Value;
        }
    }
}