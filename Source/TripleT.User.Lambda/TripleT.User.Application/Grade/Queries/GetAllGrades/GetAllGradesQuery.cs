using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Grade.Queries.GetAllGrades
{
    public class GetAllGradesQuery : IRequest<GetAllGradesViewModel>
    {
    
    }

    public class GetAllGradesQueryHandler : IRequestHandler<GetAllGradesQuery, GetAllGradesViewModel>
    {
        private readonly ILogger<GetAllGradesQueryHandler> _logger;
        private readonly IGradeRepository _gradeRepository;

        public GetAllGradesQueryHandler(ILogger<GetAllGradesQueryHandler> logger,
            IGradeRepository gradeRepository)
        {
            _logger = logger;
            _gradeRepository = gradeRepository;
        }
    
        public async Task<GetAllGradesViewModel> Handle(GetAllGradesQuery request, CancellationToken cancellationToken)
        {
            var grades = await _gradeRepository.GetAllAsync(cancellationToken);

            return new GetAllGradesViewModel
            {
                Grades = grades.Select(MapFromEntity)
            };
        }

        private GetAllGradesQueryDto MapFromEntity(GradeEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new GetAllGradesQueryDto()
            {
                Id = entity.Id, Name = entity.Name, CreatedTime = entity.CreatedTime, UpdatedTime = entity.UpdatedTime
            };
        }
    }
}