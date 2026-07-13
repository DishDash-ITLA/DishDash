using DishDash.Domain.Entities;
using DishDash.Domain.Interfaces;
using DishDash.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DishDash.Persistence.Repositories;

public class AsistenciaRepository(DishDashDbContext context) : IAsistenciaRepository
{
    public async Task<Asistencia> RegistrarAsync(Asistencia asistencia)
    {
        context.Asistencias.Add(asistencia);
        await context.SaveChangesAsync();
        return await context.Asistencias
            .Include(a => a.Empleado)
            .FirstAsync(a => a.AsistenciaId == asistencia.AsistenciaId);
    }

    public async Task<Asistencia?> ObtenerPorIdAsync(int id)
    {
        return await context.Asistencias
            .Include(a => a.Empleado)
            .FirstOrDefaultAsync(a => a.AsistenciaId == id);
    }

    public async Task<IEnumerable<Asistencia>> ObtenerPorFechaAsync(DateOnly fecha)
    {
        return await context.Asistencias
            .Include(a => a.Empleado)
            .Where(a => a.Fecha == fecha)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Asistencia>> ObtenerPorEmpleadoAsync(int empleadoId)
    {
        return await context.Asistencias
            .Include(a => a.Empleado)
            .Where(a => a.EmpleadoId == empleadoId)
            .AsNoTracking()
            .OrderByDescending(a => a.Fecha)
            .ToListAsync();
    }

    public async Task<Asistencia> ActualizarAsync(Asistencia asistencia)
    {
        context.Asistencias.Update(asistencia);
        await context.SaveChangesAsync();
        return asistencia;
    }
}