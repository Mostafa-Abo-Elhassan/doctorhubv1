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
    public class GetMoHReportsQuery: IRequest<Response<MoHReportDto>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
