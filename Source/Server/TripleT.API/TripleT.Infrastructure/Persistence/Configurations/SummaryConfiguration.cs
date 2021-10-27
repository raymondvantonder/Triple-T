using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class SummaryConfiguration : IEntityTypeConfiguration<SummaryEntity>
    {
        public void Configure(EntityTypeBuilder<SummaryEntity> builder)
        {
            builder.HasOne(x => x.SummaryCategory).WithMany(x => x.Summaries).HasForeignKey(x => x.SummaryCategoryId);
            builder.HasMany(x => x.PackageSummaryLinks).WithOne(x => x.Summary).HasForeignKey(x => x.SummaryId);
        }
    }
}
