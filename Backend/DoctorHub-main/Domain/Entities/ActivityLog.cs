namespace Domain.Entities
{



    public class ActivityLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; } = null!;   // "Created Task"
        public int? EntityId { get; set; }
        public string? EntityType { get; set; }       // Task, Project
        public DateTime LogDate { get; set; } = DateTime.Now;

        // Navigation
        public User? User { get; set; }
    }






}