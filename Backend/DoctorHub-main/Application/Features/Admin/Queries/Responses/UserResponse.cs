namespace Application.Features.Admin.Queries.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string? ProfilePhoto { get; set; }
        public float? Rating { get; set; }
    }
}
