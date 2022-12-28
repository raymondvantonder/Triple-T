using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;

namespace TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductById
{
    public class GetSummaryProductByIdQuery : IRequest<GetSummaryProductByIdQueryViewModel>
    {
        public string Id { get; set; }
    }

    public class GetSummaryProductByIdQueryHandler : IRequestHandler<GetSummaryProductByIdQuery, GetSummaryProductByIdQueryViewModel>
    {
        private readonly ISummaryProductRepository _summaryProductRepository;

        public GetSummaryProductByIdQueryHandler(ISummaryProductRepository summaryProductRepository)
        {
            _summaryProductRepository = summaryProductRepository;
        }

        public async Task<GetSummaryProductByIdQueryViewModel> Handle(GetSummaryProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productEntity = await _summaryProductRepository.GetItemByIdAsync(request.Id, cancellationToken);

            return new GetSummaryProductByIdQueryViewModel
            {
                Product = productEntity.ToDto()
            };
        }
    }
}