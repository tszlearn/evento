

using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;
using Evento.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Evento.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;

        public JwtHandler(IOptions<JwtSettings> jwtSettings) 
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JwtDto CreateToken(Guid userId, string role)
        {
            var date = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, date.ToTimestamp().ToString())
            };

            var expiries = date.AddMinutes(_jwtSettings.ExpiryMinutes);
            var singingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                claims: claims,
                expires: expiries,
                notBefore: date,
                signingCredentials: singingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto()
            {
                Token = token,
                Expires = expiries.ToTimestamp(),
            };
        }
    }
}
