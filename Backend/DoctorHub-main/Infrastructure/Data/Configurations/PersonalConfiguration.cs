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
    public class PersonalConfiguration : IEntityTypeConfiguration<Personal>
    {
        public void Configure(EntityTypeBuilder<Personal> builder)
        {


            builder.HasKey(p => p.Id);

            builder.Property(p => p.NationalId).IsRequired().HasMaxLength(14);
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(200);

            builder.Property(p => p.phoneNumber).IsRequired().HasMaxLength(20);

            builder.HasOne(p => p.Doctor)
                   .WithOne(d => d.Personal)
                   .HasForeignKey<Personal>(p => p.DoctorId)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(p => p.Patient)
                   .WithOne(pt => pt.Personal)
                   .HasForeignKey<Personal>(p => p.PatientId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
