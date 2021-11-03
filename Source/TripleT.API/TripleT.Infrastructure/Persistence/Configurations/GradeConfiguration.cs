using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<GradeEntity>
    {
        public void Configure(EntityTypeBuilder<GradeEntity> builder)
        {
            builder
                .HasMany(x => x.Packages)
                .WithOne(x => x.Grade)
                .HasForeignKey(x => x.GradeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Modules)
                .WithOne(x => x.Grade)
                .HasForeignKey(x => x.GradeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasIndex(x => x.Value)
                .IsUnique();
        }
    }
}
