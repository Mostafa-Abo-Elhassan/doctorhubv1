using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VaccinationRecord : BaseEntity<int>
    {
        public int ChildId { get; set; }
        public Child Child { get; set; } = default!;

        public string VaccineName { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateTime ScheduledAt { get; set; }
        public DateTime? AdministeredAt { get; set; }
        public string? HealthCenterId { get; set; }
        public HealthCenter? HealthCenter { get; set; }
    }

}
