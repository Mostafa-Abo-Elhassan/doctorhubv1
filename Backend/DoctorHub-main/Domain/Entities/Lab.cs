using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lab 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string LicenseNumber { get; set; }
        public string LabName { get; set; } = null!;
        public ICollection<LabResult> labResults { get; set; } = new List<LabResult>();
        public ICollection<LabOredr> labOredrs { get; set; } = new List<LabOredr>();
        public string UserId { get; set; }
        public  User user { get; set; }
    }
}
