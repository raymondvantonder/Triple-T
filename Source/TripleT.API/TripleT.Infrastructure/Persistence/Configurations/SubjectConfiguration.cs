using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<SubjectEntity>
    {
        public void Configure(EntityTypeBuilder<SubjectEntity> builder)
        {
            builder.HasMany(x => x.Modules).WithOne(x => x.Subject).HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Packages).WithOne(x => x.Subject).HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
