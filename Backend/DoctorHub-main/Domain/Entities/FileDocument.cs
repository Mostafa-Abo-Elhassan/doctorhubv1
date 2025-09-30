using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FileDocument : BaseEntity<int>
    {
        public string FileName { get; set; } = null!;
        public string Url { get; set; } = null!; // PDF/DICOM
        public DateTime UploadedAt { get; set; }
        public int LabResultId { get; set; }
        public LabResult LabResult { get; set; } = null!;
        public int LabOrederId { get; set; }
        public LabOredr LabOreder { get; set; } = null!;
        public long? Size { get; set; }

    }
}
