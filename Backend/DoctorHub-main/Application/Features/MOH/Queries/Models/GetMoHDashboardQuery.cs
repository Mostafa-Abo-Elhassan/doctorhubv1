using Application.Bases;
using Application.Features.MOH.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MOH.Queries.Models
{
    public class GetMoHDashboardQuery:IRequest<Response<MoHDashboardDto>>
    {
        // No parameters needed for dashboard
    }
}
