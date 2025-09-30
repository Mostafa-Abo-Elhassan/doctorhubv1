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
    public class FileDocumentConfiguration : IEntityTypeConfiguration<FileDocument>
    {
        public void Configure(EntityTypeBuilder<FileDocument> builder)
        {

            builder.HasKey(f => f.Id);

            builder.Property(f => f.FileName).IsRequired().HasMaxLength(255);
            builder.Property(f => f.Url).IsRequired();
            builder.Property(f => f.UploadedAt).IsRequired();

            builder.HasOne(f => f.LabResult)
                   .WithMany(r => r.fileDocuments)
                   .HasForeignKey(f => f.LabResultId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(f => f.LabOreder)
                   .WithMany(o => o.fileDocuments)
                   .HasForeignKey(f => f.LabOrederId)
                   .OnDelete(DeleteBehavior.NoAction);




        }
    }

}
