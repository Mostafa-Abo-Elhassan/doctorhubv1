using Application.Bases;
using Application.Features.Laps.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Laps.Queries.Models
{
    public class GetLabOrdersQuery: IRequest<Response<List<LabOrderDto>>>
    {
        public string Status { get; set; } // e.g., "pending"
    }
}
