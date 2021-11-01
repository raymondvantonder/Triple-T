using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Domain.Exceptions;

namespace Microsoft.EntityFrameworkCore
{
    public static class QueryableExtensions
    {
        public static Task<TEntity> MustFindAsync<TEntity>(this DbSet<TEntity> source, long primaryKey)
            where TEntity : class
        {
            return MustFindAsync(source, primaryKey, CancellationToken.None);
        }

        public static async Task<TEntity> MustFindAsync<TEntity>(this DbSet<TEntity> source, long primaryKey, CancellationToken cancellationToken)
            where TEntity : class
        {
            var entity = await source.FindAsync(new object[] { primaryKey }, cancellationToken);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Could not find entity [{typeof(TEntity).Name}] with key [{primaryKey}]");
            }

            return entity;
        }
    }
}
