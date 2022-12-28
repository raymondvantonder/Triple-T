using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence
{
    public interface ISummaryProductRepository : IRepository<SummaryProductEntity>
    {
        Task<IEnumerable<SummaryProductEntity>> GetBySubjectId(string subjectId, CancellationToken cancellationToken);
        Task<IEnumerable<SummaryProductEntity>> GetByGradeId(string gradeId, CancellationToken cancellationToken);
        Task<IEnumerable<SummaryProductEntity>> GetBySubjectAndGradeId(string subjectId, string gradeId, CancellationToken cancellationToken);
    }
}