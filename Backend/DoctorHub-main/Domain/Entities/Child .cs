using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Child : BaseEntity<int>
    {
        public string ParentId { get; set; }
        public Patient Parent { get; set; } = default!;
        public string UserId { get; set; }
        public User User { get; set; } = default!;

        public string FullName { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();

        public string? HealthCenterId { get; set; }
        public HealthCenter? HealthCenter { get; set; }

    }

}
