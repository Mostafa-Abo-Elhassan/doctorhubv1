using Application.Bases;
using Application.Features.MOH.Queries.Models;
using Application.Features.MOH.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/moh")]
    [Authorize(Roles = "Admin")]
    public class MoHController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoHController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<Response<MoHDashboardDto>>> GetDashboard(CancellationToken cancellationToken)
        {
            var query = new GetMoHDashboardQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("reports")]
        public async Task<ActionResult<Response<MoHReportDto>>> GetReports([FromQuery] DateTime from, [FromQuery] DateTime to, CancellationToken cancellationToken)
        {
            var query = new GetMoHReportsQuery { FromDate = from, ToDate = to };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
