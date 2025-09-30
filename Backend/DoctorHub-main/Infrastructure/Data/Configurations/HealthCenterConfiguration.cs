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
    public class HealthCenterConfiguration : IEntityTypeConfiguration<HealthCenter>
    {
        public void Configure(EntityTypeBuilder<HealthCenter> builder)
        {
            builder.HasOne(h => h.user)
    .WithOne(u => u.HealthCenterAdmin)
    .HasForeignKey<HealthCenter>(h => h.UserId)   // ✅ HealthCenter هو الـ dependent
    .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
