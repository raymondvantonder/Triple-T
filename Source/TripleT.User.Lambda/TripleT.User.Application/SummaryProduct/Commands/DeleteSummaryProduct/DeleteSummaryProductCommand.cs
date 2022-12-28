using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;

namespace TripleT.User.Application.SummaryProduct.Commands.DeleteSummaryProduct
{
    public class DeleteSummaryProductCommand : IRequest
    {
        public string Id { get; set; }
    }

    public class DeleteSummaryProductCommandHandler : IRequestHandler<DeleteSummaryProductCommand>
    {
        private readonly ISummaryProductRepository _summaryProductRepository;

        public DeleteSummaryProductCommandHandler(ISummaryProductRepository summaryProductRepository)
        {
            _summaryProductRepository = summaryProductRepository;
        }

        public async Task<Unit> Handle(DeleteSummaryProductCommand request, CancellationToken cancellationToken)
        {
            await _summaryProductRepository.DeleteItemAsync(request.Id, cancellationToken);
        
            return Unit.Value;
        }
    }
}