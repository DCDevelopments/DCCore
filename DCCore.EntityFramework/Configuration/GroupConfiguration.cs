using DCCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCCore.EntityFramework.Configuration
{
    internal class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        internal GroupConfiguration()
        {
            ToTable("Group");

            HasKey(x => x.GroupId)
                .Property(x => x.GroupId)
                .HasColumnName("GroupId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .HasMaxLength(256)
                .IsRequired();

            HasMany(x => x.Users)
                .WithMany(x => x.Groups)
                .Map(x =>
                {
                    x.ToTable("UserGroup");
                    x.MapLeftKey("GroupId");
                    x.MapRightKey("UserId");
                });
        }
    }

}
