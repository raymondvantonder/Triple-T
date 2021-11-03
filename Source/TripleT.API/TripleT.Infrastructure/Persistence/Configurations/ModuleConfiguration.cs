using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<ModuleEntity>
    {
        public void Configure(EntityTypeBuilder<ModuleEntity> builder)
        {
            builder.HasOne(x => x.Grade).WithMany(x => x.Modules).HasForeignKey(x => x.GradeId);
            builder.HasOne(x => x.Subject).WithMany(x => x.Modules).HasForeignKey(x => x.SubjectId);
            builder.HasOne(x => x.ModuleType).WithMany(x => x.Modules).HasForeignKey(x => x.ModuleTypeId);
            builder.HasOne(x => x.Language).WithMany(x => x.Modules).HasForeignKey(x => x.LanguageId);

            builder.HasMany(x => x.Packages).WithMany(x => x.Modules);
        }
    }
}
