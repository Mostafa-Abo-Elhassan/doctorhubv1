using Application.Features.Patients.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Queries.Responses
{
    public class PatientHistoryDto
    {
        public List<string> Diagnoses { get; set; }
        public List<string> Medications { get; set; }
        public List<LabResultDto> Labs { get; set; }
    }
}
