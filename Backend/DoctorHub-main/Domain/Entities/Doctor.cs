using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Doctor 
    {
       

        public string Id { get; set; } = Guid.NewGuid().ToString(); // توليد ID كـ string
        public string LicenseNumber { get; set; } = null!;

        // 
        public Personal Personal { get; set; } = null!;
        // ساعات العمل
       
                                                                            // تخصصه الأساسي فقط
        public MedicalSpeciality Speciality { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<LabOredr> LabOrders { get; set; } = new List<LabOredr>();

        public string UserId { get; set; }
        public  User user { get; set; }
    }
}
