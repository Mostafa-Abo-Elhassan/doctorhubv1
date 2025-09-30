using Application.Features.Doctors.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Pharmacies.Queries.Responses
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime Date { get; set; }
        public List<MedicationDto> Medications { get; set; }
    }
}
