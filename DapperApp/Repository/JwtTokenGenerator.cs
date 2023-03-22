using DapperApp.Contracts;
using DapperApp.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DapperApp.Repository
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JWTSettings _jwtSettings;
        public JwtTokenGenerator(IOptions<JWTSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var siginingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Userid),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name ),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role),
            };
            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.Now.AddSeconds(120),
                claims: claims,
                signingCredentials: siginingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
