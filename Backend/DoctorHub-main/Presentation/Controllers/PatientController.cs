
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Bases;
using Application.Features.Patients.Queries.Responses;
using Application.Features.Patients.Queries.Models;
using Application.Features.Patients.Commands.Models;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/patients")]
    [Authorize(Roles = "Patient")]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{nationalId}/dashboard")]
        public async Task<ActionResult<Response<PatientDashboardDto>>> GetDashboard(string nationalId, CancellationToken cancellationToken)
        {
            var query = new GetPatientDashboardQuery { NationalId = nationalId };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{nationalId}/appointments")]
        public async Task<ActionResult<Response<List<AppointmentDto>>>> GetAppointments(string nationalId, CancellationToken cancellationToken)
        {
            var query = new GetPatientAppointmentsQuery { PatientId = nationalId };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("/api/v1/appointments")]
        public async Task<ActionResult<Response<AppointmentCreatedDto>>> BookAppointment([FromBody] BookAppointmentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{nationalId}/records")]
        public async Task<ActionResult<Response<List<MedicalRecordDto>>>> GetMedicalRecords(string nationalId, [FromQuery] string? type, CancellationToken cancellationToken)
        {
            var query = new GetPatientMedicalRecordsQuery { NationalId = nationalId, Type = type };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}