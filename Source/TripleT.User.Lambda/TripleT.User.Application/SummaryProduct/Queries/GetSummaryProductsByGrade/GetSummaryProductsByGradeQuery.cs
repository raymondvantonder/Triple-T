using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;

namespace TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsByGrade
{
    public class GetSummaryProductsByGradeQuery : IRequest<GetSummaryProductsByGradeQueryViewModel>
    {
        public string GradeId { get; set; }
    }

    public class GetSummaryProductsByGradeQueryHandler : IRequestHandler<GetSummaryProductsByGradeQuery, GetSummaryProductsByGradeQueryViewModel>
    {
        private readonly ISummaryProductRepository _summaryProductRepository;

        public GetSummaryProductsByGradeQueryHandler(ISummaryProductRepository summaryProductRepository)
        {
            _summaryProductRepository = summaryProductRepository;
        }

        public async Task<GetSummaryProductsByGradeQueryViewModel> Handle(GetSummaryProductsByGradeQuery request, CancellationToken cancellationToken)
        {
            var productEntities = await _summaryProductRepository.GetByGradeId(request.GradeId, cancellationToken);

            return new GetSummaryProductsByGradeQueryViewModel
            {
                Products = productEntities == null ? Array.Empty<SummaryProductDto>() : productEntities.Select(x => x.ToDto())
            };
        }
    }
}