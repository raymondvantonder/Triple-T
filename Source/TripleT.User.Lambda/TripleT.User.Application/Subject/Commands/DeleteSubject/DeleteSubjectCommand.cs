using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;

namespace TripleT.User.Application.Subject.Commands.DeleteSubject
{
    public class DeleteSubjectCommand : IRequest 
    {
        public string Id { get; set; }
    }

    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand>
    {
        private readonly ISubjectRepository _subjectRepository;

        public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
    
        public async Task<Unit> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            await _subjectRepository.DeleteItemAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}