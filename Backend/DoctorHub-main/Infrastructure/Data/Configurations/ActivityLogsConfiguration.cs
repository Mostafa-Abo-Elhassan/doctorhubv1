using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class ActivityLogsConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {

            // ActivityLogs

            builder.HasOne(l => l.User)
            .WithMany(u => u.ActivityLogs)
            .HasForeignKey(l => l.UserId);
        }


    }


}
