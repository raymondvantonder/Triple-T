using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Infrastructure.Persistence
{
    public class TripleTDbContextFactory : DesignTripleTDbContextFactoryBase<TripleTDbContext>
    {
        protected override TripleTDbContext CreateNewInstance(DbContextOptions<TripleTDbContext> options)
        {
            return new TripleTDbContext(options);
        }
    }
}
