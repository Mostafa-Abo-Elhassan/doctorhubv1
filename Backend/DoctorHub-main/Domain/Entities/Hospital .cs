using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hospital 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string HospitalName { get; set; } = default!;
        public string LicenseNumber { get; set; } = default!;
        public string Type { get; set; } = "Government"; // حكومي/خاص
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public string? UserId { get; set; }   // ✅ خليها string (FK)
        public User? User { get; set; }       // ✅ Navigation Property

        // علاقات
        public ICollection<User> Staff { get; set; } = new List<User>();
    }

}
