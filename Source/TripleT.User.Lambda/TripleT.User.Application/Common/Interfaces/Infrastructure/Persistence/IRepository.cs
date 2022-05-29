using System.Threading;
using System.Threading.Tasks;
using TripleT.User.Domain.Domain;

namespace TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> AddItemAsync(TEntity item, CancellationToken cancellationToken);
        Task DeleteItemAsync(string id, CancellationToken cancellationToken);
        Task<TEntity> GetItemByIdAsync(string id, CancellationToken cancellationToken);
        Task UpdateItemAsync(TEntity item, CancellationToken cancellationToken);
    }
}