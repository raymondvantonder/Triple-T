using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class PackageConfiguration : IEntityTypeConfiguration<PackageEntity>
    {
        public void Configure(EntityTypeBuilder<PackageEntity> builder)
        {
            builder.HasOne(x => x.Grade).WithMany(x => x.Packages).HasForeignKey(x => x.GradeId);
            builder.HasOne(x => x.Subject).WithMany(x => x.Packages).HasForeignKey(x => x.SubjectId);
        }
    }
}
