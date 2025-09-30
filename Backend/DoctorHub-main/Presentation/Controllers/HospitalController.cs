using Application.Bases;
using Application.Features.Hospitals.Queries.Models;
using Application.Features.Hospitals.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/hospitals")]
    [Authorize(Roles = "Hospital")]
    public class HospitalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HospitalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("emergency/{nationalId}")]
        public async Task<ActionResult<Response<EmergencyPatientDto>>> GetEmergencyPatient(string nationalId, CancellationToken cancellationToken)
        {
            var query = new GetEmergencyPatientQuery { NationalId = nationalId };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{hospitalId}/patients")]
        public async Task<ActionResult<Response<List<HospitalPatientDto>>>> GetHospitalPatients(string hospitalId, CancellationToken cancellationToken)
        {
            var query = new GetHospitalPatientsQuery { HospitalId = hospitalId };
            var result = await _mediator.Send(query, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
