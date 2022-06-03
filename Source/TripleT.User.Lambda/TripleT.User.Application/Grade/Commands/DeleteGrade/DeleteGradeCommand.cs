using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;

namespace TripleT.User.Application.Grade.Commands.DeleteGrade
{
    public class DeleteGradeCommand : IRequest 
    {
        public string Id { get; set; }
    }

    public class DeleteGradeCommandHandler : IRequestHandler<DeleteGradeCommand>
    {
        private readonly ILogger<DeleteGradeCommandHandler> _logger;
        private readonly IGradeRepository _gradeRepository;

        public DeleteGradeCommandHandler(ILogger<DeleteGradeCommandHandler> logger,
            IGradeRepository gradeRepository)
        {
            _logger = logger;
            _gradeRepository = gradeRepository;
        }
    
        public async Task<Unit> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
        {
            await _gradeRepository.DeleteItemAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}