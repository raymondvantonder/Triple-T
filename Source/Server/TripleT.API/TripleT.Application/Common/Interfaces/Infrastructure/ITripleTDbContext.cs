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
        DbSet<PackageSummaryLinkEntity> PackageSummariesLink { get; set; }
        DbSet<SummaryEntity> Summaries { get; set; }
        DbSet<SummaryCategoryEntity> SummaryCategories { get; set; }
        DbSet<SummaryPackageEntity> SummaryPackages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
