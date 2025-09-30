using Application.Bases;
using Application.Features.Doctors.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Queries.Models
{
    public class GetDoctorDashboardQuery:IRequest<Response<DoctorDashboardDto>>
    {
        public string DoctorId { get; set; }
    }
}
