using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class SummaryPackageConfiguration : IEntityTypeConfiguration<SummaryPackageEntity>
    {
        public void Configure(EntityTypeBuilder<SummaryPackageEntity> builder)
        {
            builder.HasOne(x => x.SummaryCategory).WithMany(x => x.SummaryPackages).HasForeignKey(x => x.SummaryCategoryId);
            builder.HasMany(x => x.PackageSummaryLinks).WithOne(x => x.SummaryPackage).HasForeignKey(x => x.SummaryPackageId);
        }
    }
}
