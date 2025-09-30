using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MOH.Queries.Responses
{
    public class MoHReportDto
    {
        public string Period { get; set; }
        public List<Patient> NewCases { get; set; }
        public List<LabResult> LabTests { get; set; }
        public List<Prescription> Prescriptions { get; set; }
    }
}
