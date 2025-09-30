using Application.Bases;
using Application.Features.Pharmacies.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Pharmacies.Queries.Models
{
    public class GetPrescriptionsQuery:IRequest<Response<List<PrescriptionDto>>>
    {
        public string Status { get; set; } // e.g., "pending"
    }
}
