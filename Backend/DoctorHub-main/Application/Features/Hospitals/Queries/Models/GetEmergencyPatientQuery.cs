using Application.Bases;
using Application.Features.Hospitals.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hospitals.Queries.Models
{
    public class GetEmergencyPatientQuery: IRequest<Response<EmergencyPatientDto>>
    {
        public string NationalId { get; set; }
    }
}
