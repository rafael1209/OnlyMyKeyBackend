using Google.Apis.Auth;

namespace OnlyMyKeyBackend.Interfaces
{
    public interface IGoogleAuthService
    {
        Uri GetGoogleAuthUrl();
        Task<GoogleJsonWebSignature.Payload?> HandleGoogleCallbackAsync(string code);
    }
}
