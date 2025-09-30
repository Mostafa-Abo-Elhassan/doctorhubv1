//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using TripPool.Core.Features.Admin.Commands.Models;
//using TripPool_Core.Features.Admin.Commands.Models;
//using TripPool_Core.Features.Admin.Queries.Models;

//namespace TripPool.API.Controllers
//{
//    [Route("api/admin")]
//    [ApiController]
//    [Authorize(Roles = "Admin")]
//    public class AdminController : ControllerBase
//    {
//        private readonly IMediator _mediator;

//        public AdminController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        #region Dashboard
//        [HttpGet("dashboard")]
//        public async Task<IActionResult> GetDashboard()
//        {
//            var query = new GetDashboardQuery();
//            var response = await _mediator.Send(query);
//            return StatusCode((int)response.StatusCode, response);
//        }
//        #endregion

//        #region User Management
//        [HttpGet("users")]
//        public async Task<IActionResult> GetUsers([FromQuery] string role = null, [FromQuery] bool? isApproved = null)
//        {
//            var query = new GetUsersQuery { Role = role, IsApproved = isApproved };
//            var response = await _mediator.Send(query);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpGet("users/{userId}")]
//        public async Task<IActionResult> GetUser(string userId)
//        {
//            var query = new GetUserDetailsQuery { UserId = userId };
//            var response = await _mediator.Send(query);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpPut("users/{userId}")]
//        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserCommand command)
//        {
//            command.UserId = userId;
//            var response = await _mediator.Send(command);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpPost("users")]
//        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
//        {
//            var response = await _mediator.Send(command);
//            return StatusCode((int)response.StatusCode, response);
//        }
//        #endregion

//        #region Admin Management
//        [HttpPost("admins")]
//        public async Task<IActionResult> AddAdmin([FromBody] AddAdminCommand command)
//        {
//            var response = await _mediator.Send(command);
//            return StatusCode((int)response.StatusCode, response);
//        }
//        #endregion

//        #region Driver Approval
//        [HttpGet("drivers/pending")]
//        public async Task<IActionResult> GetPendingDrivers()
//        {
//            var query = new GetPendingDriversQuery();
//            var response = await _mediator.Send(query);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpGet("drivers/{driverId}")]
//        public async Task<IActionResult> GetDriverDetails(string driverId)
//        {
//            var query = new GetDriverDetailsQuery { DriverId = driverId };
//            var response = await _mediator.Send(query);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpPut("drivers/{driverId}/approve")]
//        public async Task<IActionResult> ApproveDriver(string driverId)
//        {
//            var command = new ApproveDriverCommand { DriverId = driverId };
//            var response = await _mediator.Send(command);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpPut("drivers/{driverId}/reject")]
//        public async Task<IActionResult> RejectDriver(string driverId, [FromBody] RejectDriverCommand command)
//        {
//            command.DriverId = driverId;
//            var response = await _mediator.Send(command);
//            return StatusCode((int)response.StatusCode, response);
//        }
//        #endregion

//        #region Trip Management
//        [HttpGet("trips")]
//        public async Task<IActionResult> GetTrips([FromQuery] string status = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
//        {
//            var query = new GetTripsQuery { Status = status, StartDate = startDate, EndDate = endDate };
//            var response = await _mediator.Send(query);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpGet("trips/{tripId}")]
//        public async Task<IActionResult> GetTripDetails(int tripId)
//        {
//            var query = new GetTripDetailsQuery { TripId = tripId };
//            var response = await _mediator.Send(query);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpPut("trips/{tripId}/cancel")]
//        public async Task<IActionResult> CancelTrip(int tripId, [FromBody] CancelTripCommand command)
//        {
//            command.TripId = tripId;
//            var response = await _mediator.Send(command);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpPut("trips/{tripId}/dispute")]
//        public async Task<IActionResult> ResolveTripDispute(int tripId, [FromBody] ResolveTripDisputeCommand command)
//        {
//            command.TripId = tripId;
//            var response = await _mediator.Send(command);
//            return StatusCode((int)response.StatusCode, response);
//        }
//        #endregion

//        #region Analytics and Reports
//        [HttpGet("analytics/usage")]
//        public async Task<IActionResult> GetUsageAnalytics([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
//        {
//            var query = new GetUsageAnalyticsQuery { StartDate = startDate, EndDate = endDate };
//            var response = await _mediator.Send(query);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpGet("analytics/drivers")]
//        public async Task<IActionResult> GetDriverAnalytics()
//        {
//            var query = new GetDriverAnalyticsQuery();
//            var response = await _mediator.Send(query);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpGet("analytics/vehicles")]
//        public async Task<IActionResult> GetVehicleAnalytics()
//        {
//            var query = new GetVehicleAnalyticsQuery();
//            var response = await _mediator.Send(query);
//            return StatusCode((int)response.StatusCode, response);
//        }

//        [HttpGet("reports")]
//        public async Task<IActionResult> GenerateReport([FromQuery] string format = "PDF", [FromQuery] string region = null, [FromQuery] string userType = null, [FromQuery] DateTime? date = null)
//        {
//            var query = new GenerateReportQuery { Format = format, Region = region, UserType = userType, Date = date };
//            var response = await _mediator.Send(query);
//            if (response.Succeeded && !string.IsNullOrEmpty(response.Data))
//            {
//                return File(Convert.FromBase64String(response.Data), "application/pdf", "TripPoolReport.pdf");
//            }
//            return StatusCode((int)response.StatusCode, response);
//        }
//        #endregion
//    }
//}
