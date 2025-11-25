using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APICatalogo.Services
{
    public class GenerateAccessTokenService : ITokenService
    {
        public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _configuration)
        {
            var key = _configuration.GetSection("JWT").GetValue<string>("SecretKey") ??
                throw new ArgumentException("Secret key inválida!");
            var privateKey = Encoding.UTF8.GetBytes(key);

            var signatureCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
                Audience = _configuration.GetSection("JWT").GetValue<string>("ValidAudience"),
                Issuer = _configuration.GetSection("JWT").GetValue<string>("ValidIssuer"),
                SigningCredentials = signatureCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return token;
        }

        public string GenerateRefreshToken()
        {
            var securityRandonByte = new byte[128];
            using var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(securityRandonByte);
            var refreshToken = Convert.ToBase64String(securityRandonByte);
            return refreshToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _configuration)
        {
            var Key = _configuration["JWT:SecretKey"] ?? throw new ArgumentException("Token Inválido!");
            var privateKey = Encoding.UTF8.GetBytes(Key);

            var tokenValidatorParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(privateKey),
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidatorParameters,
                out SecurityToken securityToken);

            if(securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                    throw new SecurityTokenException("Token Inválido!");
            }
            return principal;
        }
    }
}
