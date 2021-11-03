using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities;

namespace TripleT.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasOne(x => x.Role).WithMany(x => x.Users);
            builder.HasMany(x => x.UserAudits).WithOne(x => x.User);
            
            builder.HasOne(x => x.PasswordResetDetail).WithOne(x => x.User).HasForeignKey<PasswordResetDetailEntity>(x => x.UserId);
        }
    }
}
