using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence
{
    public interface ISubjectRepository : IRepository<SubjectEntity>
    {
        Task<IEnumerable<SubjectEntity>> GetAllAsync(CancellationToken cancellationToken);
    }
}