using DishDash.Application.DTOs.ReservasDTO;
using DishDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class MesasController(IMesaService mesaService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
        => Ok(await mesaService.ObtenerTodasAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
        => Ok(await mesaService.ObtenerPorIdAsync(id));

    /// <summary>Consultar mesas disponibles para fecha, hora y número de personas</summary>
    [HttpPost("disponibles")]
    public async Task<IActionResult> ObtenerDisponibles([FromBody] DisponibilidadMesaRequestDto dto)
        => Ok(await mesaService.ObtenerDisponiblesAsync(dto));

    [HttpPost]
    [Authorize(Roles = "Administrador,Gerente")]
    public async Task<IActionResult> Crear([FromBody] CrearMesaDto dto)
    {
        var creada = await mesaService.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.MesaId }, creada);
    }

    [HttpPatch("{id:int}/estado")]
    public async Task<IActionResult> ActualizarEstado(int id, [FromBody] string nuevoEstado)
        => Ok(await mesaService.ActualizarEstadoAsync(id, nuevoEstado));
}
