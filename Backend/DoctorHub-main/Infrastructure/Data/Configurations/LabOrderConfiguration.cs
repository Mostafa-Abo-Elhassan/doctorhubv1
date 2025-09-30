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
    public class LabOrderConfiguration : IEntityTypeConfiguration<LabOredr>
    {
        public void Configure(EntityTypeBuilder<LabOredr> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Lab)
                   .WithMany(l => l.labOredrs)
                   .HasForeignKey(o => o.LabID)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Patient)
                   .WithMany()
                   .HasForeignKey(o => o.PatientId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Doctor)
                   .WithMany(d => d.LabOrders)
                   .HasForeignKey(o => o.DoctorId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }

}
