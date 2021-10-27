using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Summary.Commands.CreateSummary;
using TripleT.Application.Summary.Commands.DeleteSummary;
using TripleT.Application.Summary.Commands.UpdateSummary;
using TripleT.Application.Summary.Queries.SummariesList;
using TripleT.Application.SummaryCategory.Commands.CreateSummaryCategory;
using TripleT.Application.SummaryCategory.Commands.DeleteSummaryCategory;
using TripleT.Application.SummaryCategory.Commands.UpdateSummaryCategory;
using TripleT.Application.SummaryCategory.Queries.SummaryCategoryList;

namespace TripleT.API.Controllers
{
    public class SummaryController : BaseController
    {
        public SummaryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public Task<long> CreateSummaryAsync([FromBody] CreateSummaryCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public Task UpdateSummaryAsync([FromBody] UpdateSummaryCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{SummaryId}")]
        public Task DeleteSummaryAsync([FromRoute] DeleteSummaryCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command, cancellationToken);
        }

        [HttpGet]
        public Task GetAllSummariesAsync([FromQuery] SummariesListQuery query, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Mediator.Send(query, cancellationToken);
        }

        [HttpPost("category")]
        public Task<long> CreateCategoryAsync([FromBody] CreateSummaryCategoryCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command, cancellationToken);
        }

        [HttpPut("category")]
        public Task UpdateCategoryAsync([FromBody] UpdateSummaryCategoryCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("category/{CategoryId}")]
        public Task DeleteCategoryAsync([FromRoute] DeleteSummaryCategoryCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(command, cancellationToken);
        }

        [HttpGet("category")]
        public Task GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Mediator.Send(new SummaryCategoryListQuery(), cancellationToken);
        }

    }
}
