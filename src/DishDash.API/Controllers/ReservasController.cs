using DishDash.Application.DTOs.ReservasDTO;
using DishDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DishDash.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class ReservasController(IReservaService reservaService) : ControllerBase
{
    /// <summary>Obtener todas las reservas</summary>
    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
        => Ok(await reservaService.ObtenerTodasAsync());

    /// <summary>Obtener reservas por fecha (YYYY-MM-DD)</summary>
    [HttpGet("fecha/{fecha}")]
    public async Task<IActionResult> ObtenerPorFecha(DateOnly fecha)
        => Ok(await reservaService.ObtenerPorFechaAsync(fecha));

    /// <summary>Obtener reservas de hoy</summary>
    [HttpGet("hoy")]
    public async Task<IActionResult> ObtenerHoy()
        => Ok(await reservaService.ObtenerPorFechaAsync(DateOnly.FromDateTime(DateTime.Today)));

    /// <summary>Obtener reserva por ID</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
        => Ok(await reservaService.ObtenerPorIdAsync(id));

    /// <summary>Crear nueva reserva</summary>
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearReservaDto dto)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var creada = await reservaService.CrearAsync(dto, usuarioId);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.ReservaId }, creada);
    }

    /// <summary>Actualizar datos de la reserva</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarReservaDto dto)
        => Ok(await reservaService.ActualizarAsync(id, dto));

    /// <summary>Cambiar estado de la reserva</summary>
    [HttpPatch("{id:int}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoReservaDto dto)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await reservaService.CambiarEstadoAsync(id, dto, usuarioId));
    }

    /// <summary>Confirmar reserva</summary>
    [HttpPatch("{id:int}/confirmar")]
    public async Task<IActionResult> Confirmar(int id)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var dto = new CambiarEstadoReservaDto("Confirmada", "Confirmada por el sistema");
        return Ok(await reservaService.CambiarEstadoAsync(id, dto, usuarioId));
    }

    /// <summary>Cancelar reserva</summary>
    [HttpPatch("{id:int}/cancelar")]
    public async Task<IActionResult> Cancelar(int id, [FromBody] string? motivo)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var dto = new CambiarEstadoReservaDto("Cancelada", motivo);
        return Ok(await reservaService.CambiarEstadoAsync(id, dto, usuarioId));
    }
}