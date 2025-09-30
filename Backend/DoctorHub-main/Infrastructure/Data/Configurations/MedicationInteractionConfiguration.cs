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
    #region MedicationInteraction
    public class MedicationInteractionConfiguration : IEntityTypeConfiguration<MedicationInteraction>
    {
        public void Configure(EntityTypeBuilder<MedicationInteraction> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.MedicationAId).IsRequired().HasMaxLength(200);
            builder.Property(m => m.MedicationBId).IsRequired().HasMaxLength(200);
            builder.Property(m => m.Description).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.Severity).IsRequired().HasMaxLength(50);


            builder.HasOne(v => v.MedicationA)
        .WithOne(pi => pi.MedicationAInteraction)
          .HasForeignKey<MedicationInteraction>(v => v.MedicationAId)
          .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(v => v.MedicationB)
        .WithOne(pi => pi.MedicationBInteraction)
            .HasForeignKey<MedicationInteraction>(v => v.MedicationBId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
    #endregion
}
