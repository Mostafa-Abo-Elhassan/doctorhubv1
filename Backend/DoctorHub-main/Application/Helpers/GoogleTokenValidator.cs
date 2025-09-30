using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace Application.Helpers
{
    public sealed class GoogleTokenValidator : IGoogleTokenValidator
    {
        private readonly IConfiguration _config;

        public GoogleTokenValidator(IConfiguration config) => _config = config;

        public async Task<GoogleJsonWebSignature.Payload> ValidateAsync(string idToken)
        {
            var audiences = _config.GetSection("Authentication:Google:Audiences").Get<string[]>();
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = audiences // لازم يكون الـ ClientId اللي الفرونت استخدمه
            };

            // هترمي Exception لو التوكن غير صالح
            return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        }
    }
}
