using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MOH.Queries.Responses
{
    public class MoHDashboardDto
    {
        public List<DiseaseStatDto> DiseaseStats { get; set; }
        public VaccinationCoverageDto VaccinationCoverage { get; set; }
    }
}
