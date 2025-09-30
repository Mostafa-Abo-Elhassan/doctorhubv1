using Application.Bases;
using MediatR;

namespace Application.Features.AppUser.Commands.Models
{
    public class ChangePasswordDto : IRequest<Response<string>>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

}
