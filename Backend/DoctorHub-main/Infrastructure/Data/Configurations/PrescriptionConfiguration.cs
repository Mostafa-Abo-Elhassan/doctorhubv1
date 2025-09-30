using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
           
             





            builder.HasKey(p => p.Id);

            builder.Property(p => p.IssuedAt).IsRequired();

        

            builder.HasOne(p => p.pharmacy)
                   .WithOne(ph => ph.Prescriptions)
                   .HasForeignKey<Prescription>(p => p.pharmacyId);

            builder.HasOne(p => p.Patient)
                   .WithMany(pt => pt.Prescriptions)
                   .HasForeignKey(p => p.PatientId);

            builder.HasOne(p => p.Doctor)
                   .WithMany(d => d.Prescriptions)
                   .HasForeignKey(p => p.DoctorId);
        }
    }
}
