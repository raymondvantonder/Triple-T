using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Subject.Commands.CreateSubject
{
    public class CreateSubjectCommand : IRequest 
    {
        public string Name { get; set; }
    }

    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand>
    {
        private readonly ILogger<CreateSubjectCommandHandler> _logger;
        private readonly ISubjectRepository _subjectRepository;

        public CreateSubjectCommandHandler(ILogger<CreateSubjectCommandHandler> logger,
            ISubjectRepository subjectRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }
    
        public async Task<Unit> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var existingSubjects = await _subjectRepository.GetAllAsync(cancellationToken);

            if (existingSubjects != null && existingSubjects.Any(x => x.Name.Equals(request.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                _logger.LogWarning($"Cannot create subject, subject [{request.Name}] already exists");
            
                throw new InvalidOperationException($"Cannot create subject, subject [{request.Name}] already exists");
            }
        
            var subjectEntity = new SubjectEntity()
            {
                Id = Guid.NewGuid().ToString(), Name = request.Name
            };

            await _subjectRepository.AddItemAsync(subjectEntity, cancellationToken);
        
            return Unit.Value;
        }
    }
}