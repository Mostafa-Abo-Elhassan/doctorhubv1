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

    #region MoHReport
    public class MoHReportConfiguration : IEntityTypeConfiguration<MoHReport>
    {
        public void Configure(EntityTypeBuilder<MoHReport> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.ReportDate).IsRequired();
            builder.Property(r => r.EarlyWarning).HasMaxLength(500);


            builder.HasOne(r => r.CreatedBy)
                   .WithMany(d => d.MoHReportAdmin)
                   .HasForeignKey(r => r.CreatedByUserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
    #endregion
}
