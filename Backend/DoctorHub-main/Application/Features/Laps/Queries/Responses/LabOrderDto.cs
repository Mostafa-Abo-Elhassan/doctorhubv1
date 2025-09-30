using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Laps.Queries.Responses
{
    public class LabOrderDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string TestType { get; set; }
        public string DoctorName { get; set; }
    }
}
