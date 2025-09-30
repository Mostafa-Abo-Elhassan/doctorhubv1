using Application.Bases;
using Application.Features.Patients.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.Models
{
    public class GetPatientAppointmentsQuery : IRequest<Response<List<AppointmentDto>>>
    {
        public string PatientId { get; set; }
    }
}
