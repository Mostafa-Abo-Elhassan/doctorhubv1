using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Prescription : BaseEntity<int>
    {

        //التاريخ/الوقت اللي اتكتبت فيه الروشتة
        public DateTime IssuedAt { get; set; } = DateTime.Now;

        public string PatientId { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
        public string? Notes { get; set; }

        public string DoctorId { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        // هنا نحدد التخصص اللي اتكتبت فيه الروشتة
        public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
        public string pharmacyId { get; set; }

        public Pharmacy pharmacy { get; set; }


    }
}
