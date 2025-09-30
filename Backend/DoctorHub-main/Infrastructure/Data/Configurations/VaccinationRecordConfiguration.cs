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
    #region VaccinationRecord
    public class VaccinationRecordConfiguration : IEntityTypeConfiguration<VaccinationRecord>
    {
        public void Configure(EntityTypeBuilder<VaccinationRecord> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.VaccineName).IsRequired().HasMaxLength(200);
            builder.Property(v => v.ScheduledAt).IsRequired();
            builder.Property(v => v.Status).IsRequired().HasMaxLength(50);

            builder.HasOne(v => v.Child)
                   .WithMany(c => c.VaccinationRecords)
                   .HasForeignKey(v => v.ChildId)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(c => c.HealthCenter)
                   .WithMany(p => p.VaccinationRecords)
                   .HasForeignKey(c => c.HealthCenterId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
    #endregion

}
