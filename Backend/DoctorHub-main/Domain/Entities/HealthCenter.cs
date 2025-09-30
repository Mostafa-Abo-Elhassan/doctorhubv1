using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HealthCenter
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string LicenseNumber { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();
        public ICollection<Child> Children { get; set; } = new List<Child>();
        public string UserId { get; set; }
        public User user { get; set; }
        public ICollection<User> Staff { get; set; } = new List<User>();

    }
}
