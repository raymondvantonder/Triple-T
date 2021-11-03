using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class UserAuditConfiguration : IEntityTypeConfiguration<UserAuditEntity>
    {
        public void Configure(EntityTypeBuilder<UserAuditEntity> builder)
        {
            builder.HasOne(x => x.User).WithMany(x => x.UserAudits);
        }
    }
}
