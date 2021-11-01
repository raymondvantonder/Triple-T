using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Domain.Entities;

namespace TripleT.Application.Common.Interfaces.Infrastructure
{
    public interface ITripleTDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<UserAuditEntity> UserAudits { get; set; }
        DbSet<GradeEntity> Grades { get; set; }
        DbSet<ModuleTypeEntity> ModuleTypes { get; set; }
        DbSet<ModuleEntity> Modules { get; set; }
        DbSet<SubjectEntity> Subjects { get; set; }
        DbSet<PackageEntity> Packages { get; set; }
        DbSet<LanguageEntity> Languages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
