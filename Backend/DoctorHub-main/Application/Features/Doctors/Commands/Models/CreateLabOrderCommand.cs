using Application.Bases;
using Application.Features.Doctors.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Commands.Models
{
    public class CreateLabOrderCommand:IRequest<Response<LabOrderCreatedDto>>
    {
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string TestType { get; set; }
        public string? Notes { get; set; }
    }
}
