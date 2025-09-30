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
    #region Doctor
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.LicenseNumber).IsRequired();
            builder.Property(d => d.Speciality).HasConversion<string>().IsRequired();
         


            // Fix the One-to-One relationship
            builder.HasOne(d => d.user)
                   .WithOne(u => u.Doctor) // Reference the Doctor property in User
                   .HasForeignKey<Doctor>(d => d.UserId) // UserId is FK in Doctor table
                   .IsRequired() // Doctor must have a User
                   .OnDelete(DeleteBehavior.NoAction); // Prevent deletion cascade

            // Optional: Configure the index for better performance
            builder.HasIndex(d => d.UserId).IsUnique();
        }
    }
    #endregion
}
