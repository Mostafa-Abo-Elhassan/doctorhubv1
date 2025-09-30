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
    #region Hospital
    public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.HospitalName).IsRequired().HasMaxLength(200);
            builder.Property(h => h.LicenseNumber).IsRequired().HasMaxLength(50);
            builder.Property(h => h.Address).IsRequired().HasMaxLength(500);
            builder.Property(h => h.PhoneNumber).HasMaxLength(20);

            builder.HasOne(h => h.User)
                   .WithOne(f => f.HospitalAdmin)
                   .HasForeignKey<Hospital>(h => h.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
    #endregion
}
