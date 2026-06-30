using DishDash.Application.DTOs.UsuarioDTO;
using DishDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishDash.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class UsuariosController(IUsuarioService usuarioService) : ControllerBase
{
    /// <summary>Obtener todos los usuarios</summary>
    [HttpGet]
    [Authorize(Roles = "Administrador,Gerente")]
    public async Task<IActionResult> ObtenerTodos()
        => Ok(await usuarioService.ObtenerTodosAsync());

    /// <summary>Obtener usuario por ID</summary>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrador,Gerente")]
    public async Task<IActionResult> ObtenerPorId(int id)
        => Ok(await usuarioService.ObtenerPorIdAsync(id));

    /// <summary>Crear nuevo usuario</summary>
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Crear([FromBody] CrearUsuarioDto dto)
    {
        var creado = await usuarioService.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.UsuarioId }, creado);
    }

    /// <summary>Actualizar usuario</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarUsuarioDto dto)
        => Ok(await usuarioService.ActualizarAsync(id, dto));

    /// <summary>Desactivar usuario (soft delete)</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await usuarioService.EliminarAsync(id);
        return NoContent();
    }
}