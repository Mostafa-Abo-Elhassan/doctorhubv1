using Application.Bases;
using Application.Features.Laps.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Laps.Commands.Models
{
    public class UploadLabResultCommand: IRequest<Response<LabResultCreatedDto>>
    {
        public int OrderId { get; set; }
        public string UploadedBy { get; set; } // Lab ID
        public string TestType { get; set; }
        public IFormFile File { get; set; }
        public string? Notes { get; set; }
    }
}
