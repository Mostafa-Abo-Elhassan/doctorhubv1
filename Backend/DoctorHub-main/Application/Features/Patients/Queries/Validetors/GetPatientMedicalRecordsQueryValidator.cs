using Application.Features.Patients.Queries.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.Validetors
{
    public class GetPatientMedicalRecordsQueryValidator : AbstractValidator<GetPatientMedicalRecordsQuery>
    {
        public GetPatientMedicalRecordsQueryValidator()
        {
            RuleFor(x => x.NationalId).NotEmpty().WithMessage("National ID is required.");
            RuleFor(x => x.Type)
                .Must(x => x == null || x == "lab" || x == "radiology" || x == "prescriptions")
                .WithMessage("Invalid record type.");
        }
    }
}
