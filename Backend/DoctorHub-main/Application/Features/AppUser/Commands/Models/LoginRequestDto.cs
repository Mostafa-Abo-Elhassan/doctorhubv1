using Application.Bases;
using MediatR;

namespace Application.Features.AppUser.Commands.Models
{
    public class LoginRequestDto : IRequest<Response<AuthResponseDto>>
    {
       
            public string NationalId { get; set; } = default!;
            public string Password { get; set; } = default!;
        

    }
}
