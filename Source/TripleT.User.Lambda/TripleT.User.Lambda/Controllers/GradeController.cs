using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripleT.User.Application.Grade.Commands.CreateGrade;
using TripleT.User.Application.Grade.Commands.DeleteGrade;
using TripleT.User.Application.Grade.Commands.UpdateGrade;
using TripleT.User.Application.Grade.Queries.GetAllGrades;

namespace TripleT.User.Lambda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GradeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task CreateAsync(CreateGradeCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }
        
        [HttpPut]
        public async Task UpdateAsync(UpdateGradeCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }
        
        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new DeleteGradeCommand {Id = id};
            
            await _mediator.Send(command, cancellationToken);
        }
        
        [HttpGet]
        public async Task<GetAllGradesViewModel> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllGradesQuery(), cancellationToken);
        }
    }

}