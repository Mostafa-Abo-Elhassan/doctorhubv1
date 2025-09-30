using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pharmacy 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // وقت استلام المريض العلاج من الصيدلية 
        public string LicenseNumber { get; set; } = null!;
        public DateTime DispensedAt { get; set; }
        public string PharmacyName { get; set; } = null!;
        // العلاقة Many-to-Many باستخدام الجدول الوسيط
        public Prescription Prescriptions { get; set; } 
        public string? PharmacyAdress { get; set; } = null!;

        public string UserId { get; set; }
        public  User user { get; set; }


    }
}
