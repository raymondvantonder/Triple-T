using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class PackageSummaryLinkConfiguration : IEntityTypeConfiguration<PackageSummaryLinkEntity>
    {
        public void Configure(EntityTypeBuilder<PackageSummaryLinkEntity> builder)
        {
            builder.HasOne(x => x.Summary).WithMany(x => x.PackageSummaryLinks).HasForeignKey(x => x.SummaryId);
            builder.HasOne(x => x.SummaryPackage).WithMany(x => x.PackageSummaryLinks).HasForeignKey(x => x.SummaryPackageId);
        }
    }
}
