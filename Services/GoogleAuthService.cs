using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using OnlyMyKeyBackend.Interfaces;

namespace OnlyMyKeyBackend.Services
{
    public class GoogleAuthService(IConfiguration configuration) : IGoogleAuthService
    {
        private readonly string? _clientId = configuration["GoogleAuthCredentials:ClientId"];
        private readonly string? _clientSecret = configuration["GoogleAuthCredentials:ClientSecret"];
        private readonly string? _redirectUri = configuration["GoogleAuthCredentials:RedirectUri"];

        public Uri GetGoogleAuthUrl()
        {
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret
                },
                Scopes = ["email", "profile"]
            });

            return flow.CreateAuthorizationCodeRequest(_redirectUri).Build();
        }

        public async Task<GoogleJsonWebSignature.Payload?> HandleGoogleCallbackAsync(string code)
        {
            try
            {
                var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = _clientId,
                        ClientSecret = _clientSecret
                    }
                });

                var tokenResponse = await flow.ExchangeCodeForTokenAsync("me", code, _redirectUri, CancellationToken.None);

                var payload = await GoogleJsonWebSignature.ValidateAsync(tokenResponse.IdToken);

                return payload;
            }
            catch
            {
                return null;
            }
        }
    }
}
