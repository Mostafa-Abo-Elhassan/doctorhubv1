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
    public class CreatePrescriptionCommand: IRequest<Response<PrescriptionCreatedDto>>
    {
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public List<MedicationDto> Medications { get; set; }
        public string? Notes { get; set; }
    }
}
