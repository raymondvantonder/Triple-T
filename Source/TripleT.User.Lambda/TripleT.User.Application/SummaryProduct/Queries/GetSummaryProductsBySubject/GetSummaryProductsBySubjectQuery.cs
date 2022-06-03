using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;

namespace TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsBySubject
{
    public class GetSummaryProductsBySubjectQuery : IRequest<GetSummaryProductsBySubjectQueryViewModel>
    {
        public string SubjectId { get; set; }
    }

    public class GetSummaryProductsBySubjectQueryHandler : IRequestHandler<GetSummaryProductsBySubjectQuery, GetSummaryProductsBySubjectQueryViewModel>
    {
        private readonly ISummaryProductRepository _summaryProductRepository;

        public GetSummaryProductsBySubjectQueryHandler(ISummaryProductRepository summaryProductRepository)
        {
            _summaryProductRepository = summaryProductRepository;
        }
    
        public async Task<GetSummaryProductsBySubjectQueryViewModel> Handle(GetSummaryProductsBySubjectQuery request, CancellationToken cancellationToken)
        {
            var productEntities = await _summaryProductRepository.GetBySubjectId(request.SubjectId, cancellationToken);

            return new GetSummaryProductsBySubjectQueryViewModel
            {
                Products = productEntities == null ? Array.Empty<SummaryProductDto>() : productEntities.Select(x => x.ToDto())
            };
        }
    }
}