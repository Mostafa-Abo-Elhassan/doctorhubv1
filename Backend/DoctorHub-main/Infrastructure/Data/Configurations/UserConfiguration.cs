using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {



        // Users
        builder.HasIndex(u => u.UserName)
            .IsUnique();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasOne(p => p.Hospital)
  .WithMany(a => a.Staff)
  .HasForeignKey(p => p.HospitalId)
  .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.HealthCenter)
                       .WithMany(a => a.Staff)
                       .HasForeignKey(p => p.HealthCenterId)
                       .OnDelete(DeleteBehavior.NoAction);

    }
}


