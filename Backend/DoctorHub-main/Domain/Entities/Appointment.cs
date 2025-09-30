using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Appointment : BaseEntity<int>
    {
        public DateTime ScheduledAt { get; set; }
        public string Mode { get; set; } = "InPerson"; // Telemedicine or InPerson
        public string Status { get; set; } = "Scheduled";// Scheduled, Completed, Cancelled

        public string PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public string DoctorId { get; set; } = string.Empty;
        public Doctor Doctor { get; set; } = null!;

       // public MedicalSpeciality Speciality { get; set; }
    }
}
