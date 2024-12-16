namespace OnlyMyKeyBackend.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId);
    }
}
