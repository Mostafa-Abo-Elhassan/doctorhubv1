using Application.Bases;
using Application.Features.Patients.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Commands.Models
{
    public class BookAppointmentCommand : IRequest<Response<AppointmentCreatedDto>>
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // InPerson, Telemedicine
    }
}
