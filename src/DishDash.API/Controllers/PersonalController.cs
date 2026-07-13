using DishDash.Application.DTOs.PersonalDTO;
using DishDash.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DishDash.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Protege todo el controlador por defecto
public class PersonalController(IPersonalService personalService) : ControllerBase
{
    private int ObtenerUsuarioAutenticadoId()
    {
        // En un JWT estándar, el NameIdentifier suele almacenar el ID del usuario
        var claim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("id");
        return int.TryParse(claim?.Value, out int id) ? id : 0;
    }

    // ── Empleados ────────────────────────────────────────────

    [HttpGet("empleados")]
    [Authorize(Roles = "Administrador,Gerente")]
    public async Task<IActionResult> ObtenerEmpleados()
    {
        var empleados = await personalService.ObtenerEmpleadosAsync();
        return Ok(empleados);
    }

    [HttpGet("empleados/{id}")]
    [Authorize(Roles = "Administrador,Gerente")]
    public async Task<IActionResult> ObtenerEmpleado(int id)
    {
        var empleado = await personalService.ObtenerEmpleadoPorIdAsync(id);
        return Ok(empleado);
    }

    [HttpPost("empleados")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CrearEmpleado([FromBody] CrearEmpleadoDto dto)
    {
        var empleado = await personalService.CrearEmpleadoAsync(dto);
        return CreatedAtAction(nameof(ObtenerEmpleado), new { id = empleado.EmpleadoId }, empleado);
    }

    [HttpPut("empleados/{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ActualizarEmpleado(int id, [FromBody] ActualizarEmpleadoDto dto)
    {
        var empleado = await personalService.ActualizarEmpleadoAsync(id, dto);
        return Ok(empleado);
    }

    // ── Puestos y Turnos (Catálogos) ─────────────────────────

    [HttpGet("puestos")]
    public async Task<IActionResult> ObtenerPuestos()
    {
        var puestos = await personalService.ObtenerPuestosAsync();
        return Ok(puestos);
    }

    [HttpGet("turnos")]
    public async Task<IActionResult> ObtenerTurnos()
    {
        var turnos = await personalService.ObtenerTurnosAsync();
        return Ok(turnos);
    }

    // ── Asignaciones de Turnos ───────────────────────────────

    [HttpPost("turnos/asignar")]
    [Authorize(Roles = "Administrador,Gerente")]
    public async Task<IActionResult> AsignarTurno([FromBody] AsignarTurnoDto dto)
    {
        var usuarioId = ObtenerUsuarioAutenticadoId();
        var asignacion = await personalService.AsignarTurnoAsync(dto, usuarioId);
        return Ok(asignacion);
    }

    [HttpGet("turnos/asignaciones")]
    public async Task<IActionResult> ObtenerAsignaciones([FromQuery] DateOnly? fecha, [FromQuery] int? empleadoId)
    {
        // Soporta filtro por fecha o por empleado
        if (fecha.HasValue)
        {
            var asignaciones = await personalService.ObtenerAsignacionesPorFechaAsync(fecha.Value);
            return Ok(asignaciones);
        }

        if (empleadoId.HasValue)
        {
            var asignaciones = await personalService.ObtenerAsignacionesPorEmpleadoAsync(empleadoId.Value);
            return Ok(asignaciones);
        }

        return BadRequest(new { mensaje = "Debe proveer una fecha o un empleadoId para buscar asignaciones." });
    }

    // ── Asistencia ───────────────────────────────────────────

    [HttpPost("asistencia")]
    public async Task<IActionResult> RegistrarAsistencia([FromBody] RegistrarAsistenciaDto dto)
    {
        var usuarioId = ObtenerUsuarioAutenticadoId();
        var asistencia = await personalService.RegistrarAsistenciaAsync(dto, usuarioId);
        return Ok(asistencia);
    }

    [HttpPatch("asistencia/{id}/salida")]
    public async Task<IActionResult> MarcarSalida(int id, [FromBody] MarcarSalidaDto dto)
    {
        var asistencia = await personalService.MarcarSalidaAsync(id, dto);
        return Ok(asistencia);
    }

    [HttpGet("asistencia")]
    public async Task<IActionResult> ObtenerAsistencia([FromQuery] DateOnly? fecha, [FromQuery] int? empleadoId)
    {
        if (fecha.HasValue)
        {
            var asistencias = await personalService.ObtenerAsistenciasPorFechaAsync(fecha.Value);
            return Ok(asistencias);
        }

        if (empleadoId.HasValue)
        {
            var asistencias = await personalService.ObtenerAsistenciasPorEmpleadoAsync(empleadoId.Value);
            return Ok(asistencias);
        }

        return BadRequest(new { mensaje = "Debe proveer una fecha o un empleadoId para buscar asistencias." });
    }
}