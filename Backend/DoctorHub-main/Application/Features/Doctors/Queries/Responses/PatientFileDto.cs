using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Queries.Responses
{
    public class PatientFileDto
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public PatientHistoryDto History { get; set; }
    }
}
