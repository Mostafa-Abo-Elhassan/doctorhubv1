using Application.Bases;
using Application.Features.Pharmacies.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Pharmacies.Commands.Models
{
    public class DispensePrescriptionCommand:IRequest<Response<PrescriptionDispensedDto>>
    {
        public int PrescriptionId { get; set; }
        public string PharmacistId { get; set; }
        public string Status { get; set; } // e.g., "Dispensed"
    }
}
