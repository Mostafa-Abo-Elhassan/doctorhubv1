using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LabResult : BaseEntity<int>
    {

        public string LapName { get; set; } = null!; // CBC / Blood Sugar / Chest XRay
        public string? LicenseNumber { get; set; }
        public string DoctorName { get; set; } = null!;
        public string Type { get; set; } = null!; // BloodTest / XRay / MRI

        public DateTime UploadedAt { get; set; }
        public MedicalSpeciality Speciality { get; set; }
        public string PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public string LabID { get; set; }
        public Lab Lab { get; set; } = null!;
        public int LabOrderId { get; set; }
        public LabOredr LabOrder { get; set; } = null!;
        public ICollection<FileDocument> fileDocuments { get; set; } = new List<FileDocument>();
    }
}
