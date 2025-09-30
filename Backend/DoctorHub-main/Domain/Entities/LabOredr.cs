using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LabOredr : BaseEntity<int>
    {


        public string LabID { get; set; }
        public Lab Lab { get; set; } = null!;
        public string PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public string DoctorId { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public string Status { get; set; } = "Requested"; // Requested, Completed
        public string? Description { get; set; }
        public string TestType { get; set; } = default!;
        public DateTime OrderedAt { get; set; } = DateTime.Now;

        public LabResult? LabResult { get; set; }
        public ICollection<FileDocument> fileDocuments { get; set; } = new List<FileDocument>();
    }
}
