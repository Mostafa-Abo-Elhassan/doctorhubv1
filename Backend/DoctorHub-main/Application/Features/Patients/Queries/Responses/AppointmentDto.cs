using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.Responses
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
