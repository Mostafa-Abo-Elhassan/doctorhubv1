namespace Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string AdminId { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
