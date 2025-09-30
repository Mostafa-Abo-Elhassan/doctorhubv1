using Application.Bases;
using MediatR;

namespace Application.Features.AppUser.Commands.Models
{
    public class ResetPasswordDto : IRequest<Response<string>>
    {
        public string Email { get; set; }
        //public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
