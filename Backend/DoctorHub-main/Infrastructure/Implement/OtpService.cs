using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Implement
{
    public class OtpService : IOtpService
    {
        private readonly IMemoryCache _cache;

        public OtpService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string GenerateOtp(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            _cache.Set(email, otp, TimeSpan.FromMinutes(5));
            return otp;
        }

        public bool ValidateOtp(string email, string otp)
        {
            return _cache.TryGetValue(email, out string? savedOtp) && savedOtp == otp;
        }

        public void RemoveOtp(string email)
        {
            _cache.Remove(email);
        }
    }
}
