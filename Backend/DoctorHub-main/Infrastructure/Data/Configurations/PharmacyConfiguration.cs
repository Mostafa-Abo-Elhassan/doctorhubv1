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
     class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {

            builder.HasKey(p => p.Id);

            builder.Property(ph => ph.LicenseNumber).IsRequired().HasMaxLength(20);
            builder.Property(p => p.PharmacyName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.DispensedAt).IsRequired();

            builder.HasOne(p => p.user)
                   .WithOne(a => a.Pharmacy)
                   .HasForeignKey<Pharmacy>(p => p.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            


        }
    }
}
