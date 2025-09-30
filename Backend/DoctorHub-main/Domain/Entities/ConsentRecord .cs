using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    public class ConsentRecord : BaseEntity<int>
    {
        public string PatientId { get; set; }
        public Patient Patient { get; set; } = default!;

        public string GrantedToUserId { get; set; }
        public User GrantedTo { get; set; } = default!;

        public DateTime GrantedAt { get; set; } = DateTime.Now;
        public DateTime? RevokedAt { get; set; }

        public ConsentStatus Status { get; set; } = ConsentStatus.Active;
    }
}
