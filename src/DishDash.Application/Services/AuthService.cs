using DishDash.Application.DTOs.AuthDTO;
using DishDash.Application.Interfaces;
using DishDash.Domain.Exceptions;
using DishDash.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Services
{
    public class AuthService(
    IUsuarioRepository usuarioRepo,
    ITokenRevocadoRepository tokenRevocadoRepo,
    ITokenService tokenService,
    ILogger<AuthService> logger) : IAuthService
    {
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var usuario = await usuarioRepo.ObtenerPorEmailAsync(dto.Email)
                ?? throw new UnauthorizedException("Credenciales inválidas.");

            if (!usuario.Activo)
                throw new UnauthorizedException("Usuario desactivado.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                throw new UnauthorizedException("Credenciales inválidas.");

            usuario.UltimoAcceso = DateTime.UtcNow;
            await usuarioRepo.ActualizarAsync(usuario);

            var token = tokenService.GenerarToken(usuario);
            var refreshToken = tokenService.GenerarRefreshToken();
            var (_, expira) = tokenService.ObtenerClaimsToken(token);

            logger.LogInformation("Login exitoso para usuario {Email}", dto.Email);

            return new LoginResponseDto(
                Token: token,
                RefreshToken: refreshToken,
                ExpiraEn: expira,
                Usuario: new UsuarioInfoDto(
                    usuario.UsuarioId,
                    usuario.Nombre,
                    usuario.Apellido,
                    usuario.Email,
                    usuario.Rol?.Nombre ?? string.Empty
                )
            );
        }

        public async Task LogoutAsync(string jti, int usuarioId, DateTime expiraEn)
        {
            await tokenRevocadoRepo.RevocarAsync(jti, usuarioId, expiraEn);
            await tokenRevocadoRepo.LimpiarExpiradosAsync();
            logger.LogInformation("Logout exitoso para usuario {UsuarioId}", usuarioId);
        }

        public async Task<bool> TokenEstaRevocadoAsync(string jti)
            => await tokenRevocadoRepo.EstaRevocadoAsync(jti);

        public async Task CambiarPasswordAsync(int usuarioId, CambiarPasswordDto dto)
        {
            if (!dto.PasswordsCoinciden)
                throw new BusinessException("Los passwords no coinciden.");

            var usuario = await usuarioRepo.ObtenerPorIdAsync(usuarioId)
                ?? throw new NotFoundException("Usuario", usuarioId);

            if (!BCrypt.Net.BCrypt.Verify(dto.PasswordActual, usuario.PasswordHash))
                throw new BusinessException("Password actual incorrecto.");

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NuevoPassword);
            await usuarioRepo.ActualizarAsync(usuario);
        }
    }
}
