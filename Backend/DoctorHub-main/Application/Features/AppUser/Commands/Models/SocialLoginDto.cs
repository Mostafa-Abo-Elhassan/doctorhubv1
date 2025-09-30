using Application.Bases;
using MediatR;

namespace Application.Features.AppUser.Commands.Models
{
    public class SocialLoginDto : IRequest<Response<AuthResponseDto>>
    {
        public string Provider { get; set; } // "Google" or "Facebook"
        public string Token { get; set; }
    }
}
