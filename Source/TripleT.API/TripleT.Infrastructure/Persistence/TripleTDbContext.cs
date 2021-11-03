using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;
using TripleT.Domain.Entities.Common;

namespace TripleT.Infrastructure.Persistence
{
    public class TripleTDbContext : DbContext, ITripleTDbContext
    {
        public TripleTDbContext(DbContextOptions<TripleTDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserAuditEntity> UserAudits { get; set; }
        public DbSet<GradeEntity> Grades { get; set; }
        public DbSet<ModuleTypeEntity> ModuleTypes { get; set; }
        public DbSet<ModuleEntity> Modules { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<PackageEntity> Packages { get; set; }
        public DbSet<LanguageEntity> Languages { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedTime = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedTime = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
