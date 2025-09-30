
using Application.Bases;
using MediatR;

namespace Application.Features.AppUser.Commands.Models
{
    public class RegisterRequestDto : IRequest<Response<string>>
    {
        public string UserName { get; set; }
        //public string? Email { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? Role { get; set; } 
        public string? Speciality { get; set; } 
        public string? LicenseNumber { get; set; } 

        public string? PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string Password { get; set; }
        //public string Role { get; set; }
        //public string PaymentInfo { get; set; }
        //public string ConfirmPassword { get; set; }


    }
}
