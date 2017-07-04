using DCCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCCore.EntityFramework.Configuration
{
    internal class MailConfiguration : EntityTypeConfiguration<Mail>
    {
        internal MailConfiguration()
        {
            ToTable("Mail");

            HasKey(x => x.MailId)
                .Property(x => x.MailId)
                .HasColumnName("MailId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            Property(x => x.MailState)
                .HasColumnName("MailState")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.UserId)
               .HasColumnName("UserId")
               .HasColumnType("uniqueidentifier")
               .IsRequired();

                        
            HasMany(x => x.Groups)
                .WithMany(x => x.Mails)
                .Map(x =>
                {
                    x.ToTable("MailGroup");
                    x.MapLeftKey("MailId");
                    x.MapRightKey("GroupId");
                });

            HasRequired(x => x.User)
                .WithMany(x => x.Mails)
                .HasForeignKey(x => x.UserId);
        }
    }
}
