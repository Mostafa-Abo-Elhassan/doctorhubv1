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
     class PrescriptionItemConfiguration : IEntityTypeConfiguration<PrescriptionItem>
    {
        public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.MedicineName).IsRequired().HasMaxLength(200);
            builder.Property(i => i.Dosage).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Frequency).IsRequired().HasMaxLength(50);

            builder.HasOne(p => p.Prescription)
               .WithMany(i => i.Items)
               .HasForeignKey(P => P.PrescriptionId);

        }
    }
}
