using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    #region Child
    public class ChildConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);

            builder.Property(c => c.DateOfBirth).IsRequired();
            builder.Property(c => c.Gender).IsRequired().HasMaxLength(10);

            builder.HasOne(c => c.Parent)
                   .WithMany(p => p.Children)
                   .HasForeignKey(c => c.ParentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.HealthCenter)
                   .WithMany(p => p.Children)
                   .HasForeignKey(c => c.HealthCenterId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.User)
                   .WithMany(p => p.Children)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
    #endregion
}
