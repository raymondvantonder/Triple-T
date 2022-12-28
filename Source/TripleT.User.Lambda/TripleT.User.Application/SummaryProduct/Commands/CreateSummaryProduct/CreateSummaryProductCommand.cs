using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.SummaryProduct.Commands.CreateSummaryProduct
{
    public class CreateSummaryProductCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileLocation { get; set; }
        public decimal FileSizeInMb { get; set; }
        public string IconUri { get; set; }
        public decimal Price { get; set; }
        public string GradeId { get; set; }
        public string SubjectId { get; set; }
    }

    public class CreateSummaryProductCommandHandler : IRequestHandler<CreateSummaryProductCommand>
    {
        private readonly ISummaryProductRepository _summaryProductRepository;

        public CreateSummaryProductCommandHandler(ISummaryProductRepository summaryProductRepository)
        {
            _summaryProductRepository = summaryProductRepository;
        }
    
        public async Task<Unit> Handle(CreateSummaryProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new SummaryProductEntity
            {
                Description = request.Description,
                Id = Guid.NewGuid().ToString(),
                Price = request.Price,
                Title = request.Title,
                FileLocation = request.FileLocation,
                GradeId = request.GradeId,
                SubjectId = request.SubjectId,
                IconUri = request.IconUri,
                FileSizeInMb = request.FileSizeInMb
            };

            var result = await _summaryProductRepository.AddItemAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}