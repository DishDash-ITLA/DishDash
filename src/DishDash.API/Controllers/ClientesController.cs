using DishDash.Application.DTOs.ReservasDTO;
using DishDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishDash.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces("application/json")]
public class ClientesController(IClienteService clienteService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
        => Ok(await clienteService.ObtenerTodosAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
        => Ok(await clienteService.ObtenerPorIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearClienteDto dto)
    {
        var creado = await clienteService.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.ClienteId }, creado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] CrearClienteDto dto)
        => Ok(await clienteService.ActualizarAsync(id, dto));
}