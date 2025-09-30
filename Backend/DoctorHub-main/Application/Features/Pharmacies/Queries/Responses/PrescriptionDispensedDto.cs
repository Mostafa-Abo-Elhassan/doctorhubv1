using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Pharmacies.Queries.Responses
{
    public class PrescriptionDispensedDto
    {
        public int PrescriptionId { get; set; }
        public string Status { get; set; }
        public DateTime DispensedDate { get; set; }
    }
}
