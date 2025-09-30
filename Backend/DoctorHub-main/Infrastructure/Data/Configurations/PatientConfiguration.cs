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
    class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);


            // Fix the One-to-One relationship
            builder.HasOne(d => d.user)
                   .WithOne(u => u.Patient) // Reference the Doctor property in User
                   .HasForeignKey<Patient>(d => d.UserId) // UserId is FK in Doctor table
                   .IsRequired() // Doctor must have a User
                   .OnDelete(DeleteBehavior.NoAction); // Prevent deletion cascade





        }
    }
}
