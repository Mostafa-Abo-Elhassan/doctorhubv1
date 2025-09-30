using Google.Apis.Auth;

namespace Application.Helpers
{
    public interface IGoogleTokenValidator
    {
        Task<GoogleJsonWebSignature.Payload> ValidateAsync(string idToken);
    }
}
