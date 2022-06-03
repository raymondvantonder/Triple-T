using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;

namespace TripleT.User.Application.SummaryProduct.Queries.GetSummaryProductsBySubjectAndGrade
{
    public class GetSummaryProductsBySubjectAndGradeQuery : IRequest<GetSummaryProductsBySubjectAndGradeQueryViewModel>
    {
        public string SubjectId { get; set; }
        public string GradeId { get; set; }
    }

    public class GetSummaryProductsBySubjectAndGradeQueryHandler : IRequestHandler<GetSummaryProductsBySubjectAndGradeQuery, GetSummaryProductsBySubjectAndGradeQueryViewModel>
    {
        private readonly ISummaryProductRepository _summaryProductRepository;

        public GetSummaryProductsBySubjectAndGradeQueryHandler(ISummaryProductRepository summaryProductRepository)
        {
            _summaryProductRepository = summaryProductRepository;
        }
    
        public async Task<GetSummaryProductsBySubjectAndGradeQueryViewModel> Handle(GetSummaryProductsBySubjectAndGradeQuery request, CancellationToken cancellationToken)
        {
            var productEntities = await _summaryProductRepository.GetBySubjectAndGradeId(request.SubjectId, request.GradeId, cancellationToken);

            return new GetSummaryProductsBySubjectAndGradeQueryViewModel
            {
                Products = productEntities == null ? Array.Empty<SummaryProductDto>() : productEntities.Select(x => x.ToDto())
            };
        }
    }
}