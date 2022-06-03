using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;

namespace TripleT.User.Application.SummaryProduct.Commands.UpdateSummaryProduct
{
    public class UpdateSummaryProductCommand : IRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileLocation { get; set; }
        public decimal FileSizeInMb { get; set; }
        public string IconUri { get; set; }
        public decimal Price { get; set; }
        public string GradeId { get; set; }
        public string SubjectId { get; set; }
    }

    public class UpdateSummaryProductCommandHandler : IRequestHandler<UpdateSummaryProductCommand>
    {
        private readonly ISummaryProductRepository _summaryProductRepository;

        public UpdateSummaryProductCommandHandler(ISummaryProductRepository summaryProductRepository)
        {
            _summaryProductRepository = summaryProductRepository;
        }
    
        public async Task<Unit> Handle(UpdateSummaryProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _summaryProductRepository.GetItemByIdAsync(request.Id, cancellationToken);

            Guard.Against.Null(existingProduct, nameof(existingProduct), "Product does not exist");

            existingProduct.Title = request.Title;
            existingProduct.Description = request.Description;
            existingProduct.FileLocation = request.FileLocation;
            existingProduct.FileSizeInMb = request.FileSizeInMb;
            existingProduct.IconUri = request.IconUri;
            existingProduct.Price = request.Price;
            existingProduct.GradeId = request.GradeId;
            existingProduct.SubjectId = request.SubjectId;

            await _summaryProductRepository.UpdateItemAsync(existingProduct, cancellationToken);
        
            return Unit.Value;
        }
    }
}