using Application.Features.Patients.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Queries.Responses
{
    public class DoctorDashboardDto
    {
        public List<AppointmentDto> TodayAppointments { get; set; }
        public List<AIAlertDto> AIAlerts { get; set; }
    }
}
