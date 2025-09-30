namespace Application.Features.AppUser.Commands.Models
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; } // إضافة الـ Refresh Token
        public string Role { get; set; }
        public DateTime ExpiresAt { get; set; }
        // بيانات إضافية عن المستخدم
        public object? Profile { get; set; }
    }
}
