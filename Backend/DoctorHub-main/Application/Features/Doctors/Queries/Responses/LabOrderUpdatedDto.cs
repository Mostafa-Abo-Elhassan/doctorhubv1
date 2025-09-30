using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Queries.Responses
{
    public class LabOrderUpdatedDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}
