using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
       
      
        public string Role { get; set; } = "Patient";     // Patient, Doctor, Pharmacist, Lab, Hospital, Admin
        public string NationalId { get; set; } = null!;

        public Doctor? Doctor { get; set; } = null!;
        public Patient? Patient { get; set; } = null!;
        public Pharmacy? Pharmacy { get; set; } = null!;
        public Lab? Lab { get; set; } = null!;
        public Hospital? HospitalAdmin { get; set; }
        public HealthCenter? HealthCenterAdmin { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastLoginDate { get; set; }

        public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        //
         public string? HospitalId { get; set; }
        public Hospital? Hospital { get; set; }
         public string? HealthCenterId { get; set; }
        public HealthCenter? HealthCenter { get; set; }

        // إضافة الـ Refresh Tokens
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<ConsentRecord> ConsentRecords { get; set; } = new List<ConsentRecord>();
        public ICollection<MoHAlert> MoHAlertAdmin { get; set; } = new List<MoHAlert>();
        public ICollection<MoHReport> MoHReportAdmin { get; set; } = new List<MoHReport>();

        public ICollection<Child> Children { get; set; } = new List<Child>();

    }
}

