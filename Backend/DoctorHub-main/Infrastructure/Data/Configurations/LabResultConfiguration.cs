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
    public class LabResultConfiguration : IEntityTypeConfiguration<LabResult>
    {
        public void Configure(EntityTypeBuilder<LabResult> builder)
        {



            builder.HasKey(r => r.Id);

            builder.Property(r => r.LapName).IsRequired().HasMaxLength(200);
            builder.Property(r => r.DoctorName).IsRequired().HasMaxLength(200);
            builder.Property(r => r.Type).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Speciality).HasConversion<string>().IsRequired();
            builder.Property(r => r.UploadedAt).IsRequired();

            builder.HasOne(r => r.Patient)
                   .WithMany(p => p.labResults)
                   .HasForeignKey(r => r.PatientId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Lab)
                   .WithMany(l => l.labResults)
                   .HasForeignKey(r => r.LabID)
                   .OnDelete(DeleteBehavior.NoAction);

         
        }
    }
}
