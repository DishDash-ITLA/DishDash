using DishDash.Domain.Entities;
using DishDash.Domain.Interfaces;
using DishDash.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DishDash.Persistence.Repositories;

public class TurnoRepository(DishDashDbContext context) : ITurnoRepository
{
    public async Task<Turno?> ObtenerPorIdAsync(int id)
    {
        return await context.Turnos.FindAsync(id);
    }

    public async Task<IEnumerable<Turno>> ObtenerTodosAsync()
    {
        return await context.Turnos.Where(t => t.Activo).AsNoTracking().ToListAsync();
    }

    public async Task<AsignacionTurno> AsignarAsync(AsignacionTurno asignacion)
    {
        context.AsignacionesTurnos.Add(asignacion);
        await context.SaveChangesAsync();
        return await context.AsignacionesTurnos
            .Include(a => a.Empleado)
            .Include(a => a.Turno)
            .FirstAsync(a => a.AsignacionId == asignacion.AsignacionId);
    }

    public async Task<IEnumerable<AsignacionTurno>> ObtenerAsignacionesPorFechaAsync(DateOnly fecha)
    {
        return await context.AsignacionesTurnos
            .Include(a => a.Empleado)
            .Include(a => a.Turno)
            .Where(a => a.Fecha == fecha)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<AsignacionTurno>> ObtenerAsignacionesPorEmpleadoAsync(int empleadoId)
    {
        return await context.AsignacionesTurnos
            .Include(a => a.Empleado)
            .Include(a => a.Turno)
            .Where(a => a.EmpleadoId == empleadoId)
            .AsNoTracking()
            .OrderByDescending(a => a.Fecha)
            .ToListAsync();
    }
}