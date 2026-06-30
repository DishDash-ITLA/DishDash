using DishDash.Application.DTOs.AuthDTO;
using DishDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DishDash.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>Iniciar sesión</summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponseDto), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var result = await authService.LoginAsync(dto);
        return Ok(result);
    }

    /// <summary>Cerrar sesión (revoca el token JWT)</summary>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] LogoutRequestDto dto)
    {
        var expiraClaim = User.FindFirstValue("exp");
        var expira = expiraClaim is not null
            ? DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiraClaim)).UtcDateTime
            : DateTime.UtcNow.AddHours(8);

        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await authService.LogoutAsync(dto.Jti, usuarioId, expira);
        return Ok(new { mensaje = "Sesión cerrada exitosamente." });
    }

    /// <summary>Cambiar contraseña del usuario autenticado</summary>
    [HttpPost("cambiar-password")]
    [Authorize]
    public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDto dto)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await authService.CambiarPasswordAsync(usuarioId, dto);
        return Ok(new { mensaje = "Contraseña actualizada exitosamente." });
    }

    /// <summary>Obtener perfil del usuario autenticado</summary>
    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        return Ok(new
        {
            UsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            Nombre = User.FindFirstValue(ClaimTypes.Name),
            Email = User.FindFirstValue(ClaimTypes.Email),
            Rol = User.FindFirstValue(ClaimTypes.Role)
        });
    }
}