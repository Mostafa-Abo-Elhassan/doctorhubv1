using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hospitals.Queries.Responses
{
    public class EmergencyPatientDto
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public List<string> ChronicDiseases { get; set; }
        public List<string> Allergies { get; set; }
        public List<string> CurrentMedications { get; set; }
    }
}
