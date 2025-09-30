using Application.Features.Patients.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Commands.Validetors
{
    public class BookAppointmentCommandValidator : AbstractValidator<BookAppointmentCommand>
    {
        public BookAppointmentCommandValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty().WithMessage("Patient ID is required.");
            RuleFor(x => x.DoctorId).NotEmpty().WithMessage("Doctor ID is required.");
            RuleFor(x => x.Date).GreaterThan(DateTime.UtcNow).WithMessage("Appointment date must be in the future.");
            RuleFor(x => x.Type).Must(x => x == "InPerson" || x == "Telemedicine").WithMessage("Invalid appointment type.");
        }
    }
}
