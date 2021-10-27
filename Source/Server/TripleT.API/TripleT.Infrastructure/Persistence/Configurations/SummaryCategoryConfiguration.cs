using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class SummaryCategoryConfiguration : IEntityTypeConfiguration<SummaryCategoryEntity>
    {
        public void Configure(EntityTypeBuilder<SummaryCategoryEntity> builder)
        {
            builder.HasMany(x => x.Summaries).WithOne(x => x.SummaryCategory).HasForeignKey(x => x.SummaryCategoryId);
        }
    }
}
