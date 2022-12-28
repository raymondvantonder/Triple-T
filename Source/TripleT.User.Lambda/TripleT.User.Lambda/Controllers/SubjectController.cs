using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripleT.User.Application.Subject.Commands.CreateSubject;
using TripleT.User.Application.Subject.Commands.DeleteSubject;
using TripleT.User.Application.Subject.Commands.UpdateSubject;
using TripleT.User.Application.Subject.Queries.GetAllSubjects;

namespace TripleT.User.Lambda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task CreateAsync(CreateSubjectCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }
        
        [HttpPut]
        public async Task UpdateAsync(UpdateSubjectCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }
        
        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new DeleteSubjectCommand {Id = id};
            
            await _mediator.Send(command, cancellationToken);
        }
        
        [HttpGet]
        public async Task<GetAllSubjectsViewModel> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllSubjectsQuery(), cancellationToken);
        }
    }

}