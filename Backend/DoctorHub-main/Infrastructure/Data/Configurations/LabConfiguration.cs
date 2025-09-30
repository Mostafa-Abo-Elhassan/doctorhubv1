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
    public class LabConfiguration : IEntityTypeConfiguration<Lab>
    {
        public void Configure(EntityTypeBuilder<Lab> builder)
        {


            builder.HasKey(l => l.Id);

            builder.Property(l => l.LicenseNumber).HasMaxLength(50);
            builder.Property(l => l.LabName).IsRequired().HasMaxLength(200);

           

            builder.HasOne(l => l.user)
                   .WithOne(a=> a.Lab)
                   .HasForeignKey<Lab>(l => l.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
