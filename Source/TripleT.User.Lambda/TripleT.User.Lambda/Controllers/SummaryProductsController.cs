using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripleT.User.Application.SummaryProduct.Commands.CreateSummaryProduct;
using TripleT.User.Application.SummaryProduct.Commands.DeleteSummaryProduct;
using TripleT.User.Application.SummaryProduct.Commands.UpdateSummaryProduct;
using TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductById;
using TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsByGrade;
using TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsBySubject;
using TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsBySubjectAndGrade;
using TripleT.User.Infrastructure.Persistence.Models;

namespace TripleT.User.Lambda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SummaryProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SummaryProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task Create(CreateSummaryProductCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public async Task Update(UpdateSummaryProductCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteSummaryProductCommand {Id = id}, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<GetSummaryProductByIdQueryViewModel> GetById([FromRoute] string id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetSummaryProductByIdQuery {Id = id}, cancellationToken);
        }
        
        [HttpGet("grade/{id}")]
        public async Task<GetSummaryProductsByGradeQueryViewModel> GetByGradeId([FromRoute] string id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetSummaryProductsByGradeQuery() {GradeId = id}, cancellationToken);
        }
        
        [HttpGet("subject/{id}")]
        public async Task<GetSummaryProductsBySubjectQueryViewModel> GetBySubjectId([FromRoute] string id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetSummaryProductsBySubjectQuery() {SubjectId = id}, cancellationToken);
        }
        
        [HttpGet("grade/{gradeId}/subject/{subjectId}")]
        public async Task<GetSummaryProductsBySubjectAndGradeQueryViewModel> GetByGradeId([FromRoute] string gradeId, [FromRoute] string subjectId, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetSummaryProductsBySubjectAndGradeQuery
            {
                GradeId = gradeId,
                SubjectId = subjectId
            }, cancellationToken);
        }
    }
}