using Application.Bases;
using Application.Features.AppUser.Commands.Models;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtService _IJwtService;
        public AuthController(IMediator mediator, IJwtService jwtService)
        {
            _mediator = mediator;
            _IJwtService = jwtService;


        }
        //test

        [HttpPost("register")]
        public async Task<ActionResult<Response<string>>> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _mediator.Send(request);
            return StatusCode((int)result.StatusCode, result);
        }

        //[HttpPost("add-driver-role")]
        //public async Task<ActionResult<Response<AuthResponseDto>>> AddDriverRole([FromBody] AddDriverRoleCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return StatusCode((int)result.StatusCode, result);
        //}

        [HttpPost("login")]
        public async Task<ActionResult<Response<AuthResponseDto>>> Login([FromBody] LoginRequestDto request)
        {
            var result = await _mediator.Send(request);
            return StatusCode((int)result.StatusCode, result);
        }



        [HttpPost("forgot-password-otp")]
        public async Task<ActionResult<Response<string>>> ForgotPassword([FromBody] ForgotPasswordDto request)
        {
            var result = await _mediator.Send(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("verify-otp")]
        public async Task<ActionResult<Response<string>>> VerifyOtp([FromBody] otp request)

        {
            var result = await _mediator.Send(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<Response<string>>> ResetPassword([FromBody] ResetPasswordDto request)
        {
            var result = await _mediator.Send(request);
            return StatusCode((int)result.StatusCode, result);
        }

        //[Authorize] // عشان لازم يكون معاه JWT
        [HttpPost("change-password")]
        public async Task<ActionResult<Response<string>>> ChangePassword([FromBody] ChangePasswordDto request)
        {
            var result = await _mediator.Send(request);
            return StatusCode((int)result.StatusCode, result);
        }



        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            var newAccessToken = await _IJwtService.GenerateNewAccessTokenAsync(dto.RefreshToken);
            if (newAccessToken == null)
                return BadRequest("Invalid or expired refresh token");

            return Ok(new { Token = newAccessToken });
        }



        [AllowAnonymous]
        [HttpPost("google-login")]
        public async Task<ActionResult<Response<AuthResponseDto>>> GoogleLogin([FromBody] GoogleLoginDto request)
        {
            var result = await _mediator.Send(request);
            return StatusCode((int)result.StatusCode, result);
        }




        //[HttpPost("social-login")]
        //public async Task<ActionResult<Response<AuthResponseDto>>> SocialLogin([FromBody] SocialLoginDto request)
        //{
        //    var result = await _mediator.Send(request);
        //    return StatusCode((int)result.StatusCode, result);
        //}
    }



}
