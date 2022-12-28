using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence
{
    public interface IGradeRepository : IRepository<GradeEntity>
    {
        Task<IEnumerable<GradeEntity>> GetAllAsync(CancellationToken cancellationToken);
    }
}