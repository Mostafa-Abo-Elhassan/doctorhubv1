using Application.Features.Patients.Queries.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.Validetors
{
    public class GetPatientDashboardQueryValidator : AbstractValidator<GetPatientDashboardQuery>
    {
        public GetPatientDashboardQueryValidator()
        {
            RuleFor(x => x.NationalId).NotEmpty().WithMessage("National ID is required.");
        }
    }
}
