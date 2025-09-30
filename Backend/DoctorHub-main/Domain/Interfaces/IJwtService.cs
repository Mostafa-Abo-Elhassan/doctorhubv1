using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(User user);
        Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(User user); // دالة جديدة
        Task<bool> ValidateRefreshTokenAsync(string refreshToken, string userId); // للتحقق من الـ Refresh Token
        Task<string> GenerateNewAccessTokenAsync(string refreshToken); // لتوليد Access Token جديد
    }
}
