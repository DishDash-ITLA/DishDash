using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DishDash.Application.Interfaces;
using DishDash.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DishDash.Infrastructure.Services
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        public string GenerarToken(Usuario usuario)
        {
            var jwtConfig = config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jti = Guid.NewGuid().ToString();
            var expira = DateTime.UtcNow.AddHours(
                double.Parse(jwtConfig["ExpiresHours"] ?? "8"));

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.UsuarioId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Jti, jti),
            new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
            new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? string.Empty),
            new Claim("rolId", usuario.RolId.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: expira,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerarRefreshToken()
        {
            var bytes = new byte[64];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }

        public (string jti, DateTime expira) ObtenerClaimsToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var jti = jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            return (jti, jwt.ValidTo);
        }
    }
}
