using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class PasswordResetDetailConfiguration : IEntityTypeConfiguration<PasswordResetDetailEntity>
    {
        public void Configure(EntityTypeBuilder<PasswordResetDetailEntity> builder)
        {
            builder.HasOne(x => x.User).WithOne(x => x.PasswordResetDetail).HasForeignKey<UserEntity>(x => x.PasswordResetDetailId);
        }
    }
}
