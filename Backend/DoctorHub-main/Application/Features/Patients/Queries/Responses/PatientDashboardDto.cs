using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.Responses
{
    public class PatientDashboardDto
    {
        public PrescriptionDto? LastPrescription { get; set; }
        public LabResultDto? LastLabResult { get; set; }
        public AppointmentDto? UpcomingAppointment { get; set; }
    }
}
