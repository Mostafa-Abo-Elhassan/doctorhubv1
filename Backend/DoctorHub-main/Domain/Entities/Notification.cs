using Domain.Entities;

namespace Domain.Entities
{
    public class Notification : BaseEntity<int>
    {

        public string UserId { get; set; } = null!;
        // public int? TaskId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime NotifyTime { get; set; }
        public string NotificationType { get; set; } = "Reminder";
        public bool IsSent { get; set; } = false;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation
        public string PatientId { get; set; }
        public Patient? Patient { get; set; } = null!;

        public string DoctorId { get; set; }
        public Doctor? Doctor { get; set; } = null!;

        public User? User { get; set; }
        //  public Task? Task { get; set; }
    }
}
