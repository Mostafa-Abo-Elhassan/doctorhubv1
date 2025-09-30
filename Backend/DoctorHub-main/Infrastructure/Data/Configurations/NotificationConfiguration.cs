
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {


            builder.HasKey(n => n.Id);

            builder.Property(n => n.NotificationType).IsRequired().HasMaxLength(100);
            builder.Property(n => n.CreatedDate).IsRequired();

            builder.HasOne(n => n.Patient)
                   .WithMany(p => p.Notifications)
                   .HasForeignKey(n => n.PatientId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(n => n.Doctor)
                   .WithMany(d => d.Notifications)
                   .HasForeignKey(n => n.DoctorId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(n => n.User)
                   .WithMany(d => d.Notifications)
                   .HasForeignKey(n => n.UserId)
                   .OnDelete(DeleteBehavior.NoAction);


            // builder.HasOne(n => n.User)
            // .WithMany(u => u.Notifications)
            // .HasForeignKey(n => n.UserId);







        }
    }


}


