using Application.Bases;
using MediatR;

namespace Application.Features.AppUser.Commands.Models
{
    public class ForgotPasswordDto : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
