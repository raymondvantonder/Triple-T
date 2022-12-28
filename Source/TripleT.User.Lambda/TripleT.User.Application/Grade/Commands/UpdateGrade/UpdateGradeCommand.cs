using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Grade.Commands.UpdateGrade
{
    public class UpdateGradeCommand : IRequest 
    {
        public string Id { get; set; }
        public string NewName { get; set; }
    }

    public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeCommand>
    {
        private readonly ILogger<UpdateGradeCommandHandler> _logger;
        private readonly IGradeRepository _gradeRepository;

        public UpdateGradeCommandHandler(ILogger<UpdateGradeCommandHandler> logger,
            IGradeRepository gradeRepository)
        {
            _logger = logger;
            _gradeRepository = gradeRepository;
        }
    
        public async Task<Unit> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            var existingGrades = await _gradeRepository.GetAllAsync(cancellationToken);

            if (existingGrades != null && existingGrades.Any(x => x.Name.Equals(request.NewName, StringComparison.InvariantCultureIgnoreCase)))
            {
                _logger.LogWarning($"Cannot update grade, grade [{request.NewName}] already exists");
            
                throw new InvalidOperationException($"Cannot update grade, grade [{request.NewName}] already exists");
            }
        
            var gradeEntity = await _gradeRepository.GetItemByIdAsync(request.Id, cancellationToken);

            Guard.Against.Null(gradeEntity, nameof(GradeEntity));
        
            gradeEntity.Name = request.NewName;
        
            await _gradeRepository.UpdateItemAsync(gradeEntity, cancellationToken);
        
            return Unit.Value;
        }
    }
}