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
    public class GetPatientMedicalRecordsQuery : IRequest<Response<List<MedicalRecordDto>>>
    {
        public string NationalId { get; set; }
        public string? Type { get; set; } // Optional: lab, radiology, prescriptions
    }
}
