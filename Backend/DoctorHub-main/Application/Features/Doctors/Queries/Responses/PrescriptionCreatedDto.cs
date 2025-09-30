using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Queries.Responses
{
    public class PrescriptionCreatedDto
    {
        public int PrescriptionId { get; set; }
        public string Status { get; set; }
    }
}
