using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Subject.Queries.GetAllSubjects
{
    public class GetAllSubjectsQuery : IRequest<GetAllSubjectsViewModel>
    {
    
    }

    public class GetAllSubjectQueryHandler : IRequestHandler<GetAllSubjectsQuery, GetAllSubjectsViewModel>
    {
        private readonly ILogger<GetAllSubjectQueryHandler> _logger;
        private readonly ISubjectRepository _subjectRepository;

        public GetAllSubjectQueryHandler(ILogger<GetAllSubjectQueryHandler> logger,
            ISubjectRepository subjectRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }
    
        public async Task<GetAllSubjectsViewModel> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepository.GetAllAsync(cancellationToken);

            return new GetAllSubjectsViewModel
            {
                Subjects = subjects.Select(MapFromEntity)
            };
        }

        private GetAllSubjectsQueryDto MapFromEntity(SubjectEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new GetAllSubjectsQueryDto()
            {
                Id = entity.Id, Name = entity.Name, CreatedTime = entity.CreatedTime, UpdatedTime = entity.UpdatedTime
            };
        }
    }
}