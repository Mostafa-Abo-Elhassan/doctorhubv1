using Application.Bases;
using Application.Features.Patients.Queries.Responses;
using Application.Features.Pharmacies.Commands.Models;
using Application.Features.Pharmacies.Queries.Models;
using Application.Features.Pharmacies.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrescriptionDto = Application.Features.Pharmacies.Queries.Responses.PrescriptionDto;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/pharmacy")]
    [Authorize(Roles = "Pharmacist")]
    public class PharmacyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PharmacyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("prescriptions")]
        public async Task<ActionResult<Response<List<PrescriptionDto>>>> GetPendingPrescriptions([FromQuery] string status, CancellationToken cancellationToken)
        {
            var query = new GetPrescriptionsQuery { Status = status };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("/api/v1/prescriptions/{id}")]
        public async Task<ActionResult<Response<PrescriptionDto>>> GetPrescription(int id, CancellationToken cancellationToken)
        {
            var query = new GetPrescriptionByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("/api/v1/prescriptions/{id}/dispense")]
        public async Task<ActionResult<Response<PrescriptionDispensedDto>>> DispensePrescription(int id, [FromBody] DispensePrescriptionCommand command, CancellationToken cancellationToken)
        {
            command.PrescriptionId = id;
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
