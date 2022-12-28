using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Subject.Commands.UpdateSubject
{
    public class UpdateSubjectCommand : IRequest 
    {
        public string Id { get; set; }
        public string NewName { get; set; }
    }

    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand>
    {
        private readonly ILogger<UpdateSubjectCommandHandler> _logger;
        private readonly ISubjectRepository _subjectRepository;

        public UpdateSubjectCommandHandler(ILogger<UpdateSubjectCommandHandler> logger,
            ISubjectRepository subjectRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }
    
        public async Task<Unit> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var existingSubjects = await _subjectRepository.GetAllAsync(cancellationToken);

            if (existingSubjects != null && existingSubjects.Any(x => x.Name.Equals(request.NewName, StringComparison.InvariantCultureIgnoreCase)))
            {
                _logger.LogWarning($"Cannot update subject, subject [{request.NewName}] already exists");
            
                throw new InvalidOperationException($"Cannot update subject, subject [{request.NewName}] already exists");
            }
        
            var subjectEntity = await _subjectRepository.GetItemByIdAsync(request.Id, cancellationToken);

            Guard.Against.Null(subjectEntity, nameof(SubjectEntity));
        
            subjectEntity.Name = request.NewName;
        
            await _subjectRepository.UpdateItemAsync(subjectEntity, cancellationToken);
        
            return Unit.Value;
        }
    }
}