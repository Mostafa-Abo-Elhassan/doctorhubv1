using Application.Bases;
using Application.Features.Doctors.Commands.Models;
using Application.Features.Doctors.Queries.Models;
using Application.Features.Doctors.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/doctors")]
    [Authorize(Roles = "Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{doctorId}/dashboard")]
        public async Task<ActionResult<Response<DoctorDashboardDto>>> GetDashboard(string doctorId, CancellationToken cancellationToken)
        {
            var query = new GetDoctorDashboardQuery { DoctorId = doctorId };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{doctorId}/patients/{patientId}")]
        public async Task<ActionResult<Response<PatientFileDto>>> GetPatientFile(string doctorId, string patientId, CancellationToken cancellationToken)
        {
            var query = new GetPatientFileQuery { DoctorId = doctorId, PatientId = patientId };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("/api/v1/prescriptions")]
        public async Task<ActionResult<Response<PrescriptionCreatedDto>>> CreatePrescription([FromBody] CreatePrescriptionCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("/api/v1/lab-orders")]
        public async Task<ActionResult<Response<LabOrderCreatedDto>>> CreateLabOrder([FromBody] CreateLabOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("/api/v1/lab-orders/{orderId}")]
        public async Task<ActionResult<Response<LabOrderUpdatedDto>>> UpdateLabOrder(int orderId, [FromBody] UpdateLabOrderCommand command, CancellationToken cancellationToken)
        {
            command.OrderId = orderId;
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
