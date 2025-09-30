using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PrescriptionItem : BaseEntity<int>
    {

        public string MedicineName { get; set; } = null!;
        public string? Notes { get; set; }
        public string Dosage { get; set; } = null!;
        public string Frequency { get; set; } = null!;
        public string? Manufacturer { get; set; } = default!;
        public string Form { get; set; } = default!; // Tablet, Syrup وCapsuleو Injectionو Ointment
        public int? PrescriptionId { get; set; }
        public Prescription? Prescription { get; set; } = null!;
        //public MedicationInteraction MedicationInteractions { get; set; } = null!;

        // One-to-one Navigations
        public MedicationInteraction? MedicationAInteraction { get; set; }
        public MedicationInteraction? MedicationBInteraction { get; set; }
        //public ICollection<MedicationInteraction> MedicationInteractions { get; set; } = new List<MedicationInteraction>();


    }
}
