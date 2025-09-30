using Application.Bases;
using MediatR;

namespace Application.Features.AppUser.Commands.Models
{
    public sealed class GoogleLoginDto : IRequest<Response<AuthResponseDto>>
    {
        public string IdToken { get; set; } // اللي جاي من الفرونت
    }

}
