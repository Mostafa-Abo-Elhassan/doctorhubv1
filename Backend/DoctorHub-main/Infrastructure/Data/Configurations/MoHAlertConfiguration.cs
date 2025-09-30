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
    #region MoHAlert
    public class MoHAlertConfiguration : IEntityTypeConfiguration<MoHAlert>
    {
        public void Configure(EntityTypeBuilder<MoHAlert> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.AlertType).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Description).IsRequired().HasMaxLength(1000);
            builder.Property(a => a.Severity).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Status).IsRequired().HasMaxLength(50);

            builder.HasOne(a => a.CreatedBy)
              .WithMany(d => d.MoHAlertAdmin)
              .HasForeignKey(a => a.CreatedByUserId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
    #endregion
}
