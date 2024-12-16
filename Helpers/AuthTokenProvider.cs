using System.IdentityModel.Tokens.Jwt;
using OnlyMyKeyBackend.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace OnlyMyKeyBackend.Helpers
{
    public class AuthTokenProvider(IConfiguration configuration) : ITokenService
    {
        private readonly string? _secret = configuration["Jwt:SecretKey"];
        private readonly string? _issuer = configuration["Jwt:Issuer"];

        public string GenerateAccessToken(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
