using Application.Features.Patients.Queries.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.Validetors
{
    public class GetPatientAppointmentsQueryValidator : AbstractValidator<GetPatientAppointmentsQuery>
    {
        public GetPatientAppointmentsQueryValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty().WithMessage("Patient ID is required.");
        }
    }
}
