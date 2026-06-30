using DishDash.Application.DTOs.InventarioDTO;
using DishDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DishDash.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class InventarioController(IInventarioService inventarioService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
        => Ok(await inventarioService.ObtenerTodosAsync());

    [HttpGet("alertas")]
    public async Task<IActionResult> ObtenerAlertas()
        => Ok(await inventarioService.ObtenerConAlertasAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
        => Ok(await inventarioService.ObtenerPorIdAsync(id));

    [HttpPost]
    [Authorize(Roles = "Administrador,Gerente")]
    public async Task<IActionResult> Crear([FromBody] CrearProductoDto dto)
    {
        var creado = await inventarioService.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.ProductoId }, creado);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador,Gerente")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProductoDto dto)
        => Ok(await inventarioService.ActualizarAsync(id, dto));

    /// <summary>Registrar entrada o salida de inventario</summary>
    [HttpPost("{id:int}/movimientos")]
    public async Task<IActionResult> RegistrarMovimiento(int id, [FromBody] MovimientoRequestDto dto)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var movimiento = await inventarioService.RegistrarMovimientoAsync(id, dto, usuarioId);
        return Ok(movimiento);
    }

    [HttpGet("categorias")]
    public async Task<IActionResult> ObtenerCategorias()
        => Ok(await inventarioService.ObtenerCategoriasAsync());

    [HttpGet("unidades")]
    public async Task<IActionResult> ObtenerUnidades()
        => Ok(await inventarioService.ObtenerUnidadesAsync());
}