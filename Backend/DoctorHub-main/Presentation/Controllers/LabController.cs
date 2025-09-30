using Application.Bases;
using Application.Features.Laps.Commands.Models;
using Application.Features.Laps.Queries.Models;
using Application.Features.Laps.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/lab-orders")]
    [Authorize(Roles = "Lab")]
    public class LabController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LabController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<LabOrderDto>>>> GetPendingLabOrders([FromQuery] string status, CancellationToken cancellationToken)
        {
            var query = new GetLabOrdersQuery { Status = status };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("{orderId}/results")]
        public async Task<ActionResult<Response<LabResultCreatedDto>>> UploadLabResult(int orderId, [FromForm] UploadLabResultCommand command, CancellationToken cancellationToken)
        {
            command.OrderId = orderId;
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
