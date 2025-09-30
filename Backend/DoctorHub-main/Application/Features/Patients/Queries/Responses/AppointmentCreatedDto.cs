using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.Responses
{
    public class AppointmentCreatedDto
    {
        public int AppointmentId { get; set; }
        public string Status { get; set; }
    }
}
