using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Patient 
    {
        // public string NationalId { get; set; } = null!;
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public Personal Personal { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<Notification> NotSifications { get; set; } = new List<Notification>();
        public ICollection<LabResult> labResults { get; set; } = new List<LabResult>();
        public string UserId { get; set; }
        public User user { get; set; }
        //public ICollection<VaccinationRecord> Vaccinations { get; set; } = new List<VaccinationRecord>();
        public ICollection<Child> Children { get; set; } = new List<Child>();
        public ICollection<ConsentRecord> ConsentRecords { get; set; } = new List<ConsentRecord>();

    }
}
