using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Personal : BaseEntity<int>
    {

        public string NationalId { get; set; } = null!;

        public string FullName { get; set; } = default!;
        public TimeSpan? WorkStartTime { get; set; } = new TimeSpan(9, 0, 0); // مثال 9 صباحًا
        public TimeSpan? WorkEndTime { get; set; } = new TimeSpan(17, 0, 0); // مثال 5 مساءً
        public string? ClinicAddress { get; set; }
        public DaysOfWeek WorkingDays { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? DoctorId { get; set; }
        public Doctor? Doctor { get; set; } = null!;
        public string? PatientId { get; set; }
        public Patient? Patient { get; set; } = null!;

    }
}
