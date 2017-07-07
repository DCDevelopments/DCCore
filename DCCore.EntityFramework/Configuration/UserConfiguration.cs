using DCCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCCore.EntityFramework.Configuration
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        internal UserConfiguration()
        {
            ToTable("User");

            HasKey(x => x.UserId)
                .Property(x => x.UserId)
                .HasColumnName("UserId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            Property(x => x.PasswordHash)
                .HasColumnName("PasswordHash")
                .HasColumnType("nvarchar")
                .IsMaxLength()
                .IsOptional();

            Property(x => x.SecurityStamp)
                .HasColumnName("SecurityStamp")
                .HasColumnType("nvarchar")
                .IsMaxLength()
                .IsOptional();

            Property(x => x.UserName)
                .HasColumnName("UserName")
                .HasColumnType("nvarchar")
                .HasMaxLength(256)
                .IsRequired();

            Property(x => x.Email)
               .HasColumnName("Email")
               .HasColumnType("nvarchar")
               .HasMaxLength(256)
               .IsRequired();

            HasMany(x => x.Roles)
               .WithMany(x => x.Users)
               .Map(x =>
               {
                   x.ToTable("UserRole");
                   x.MapLeftKey("UserId");
                   x.MapRightKey("RoleId");
               });


            HasMany(x => x.Groups)
                .WithMany(x => x.Users)
                .Map(x =>
                {
                    x.ToTable("UserGroup");
                    x.MapLeftKey("UserId");
                    x.MapRightKey("GroupId");
                });

            HasMany(x => x.Claims)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);

            HasMany(x => x.Logins)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);

            HasMany(x => x.Mails)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
