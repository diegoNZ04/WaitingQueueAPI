using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using QueueSystem.Application.Implements.Interfaces;
using QueueSystem.Domain.Entities;

namespace QueueSystem.Application.Implements
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private const string SecretKey = "uhaE2LveYRr+T1Ay5DS0kmbhX//k+Hk+gtf8f9QSqpQ=";
        private const int ExpirationMinutes = 60;
        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "QueueSystem",
                audience: "QueueSystemUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(ExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}