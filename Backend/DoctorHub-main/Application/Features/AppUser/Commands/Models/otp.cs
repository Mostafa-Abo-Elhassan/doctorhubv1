using Application.Bases;
using MediatR;

namespace Application.Features.AppUser.Commands.Models
{
    public class otp : IRequest<Response<string>>
    {
        public string Otp { get; set; }
        public string Email { get; set; }

    }
}
