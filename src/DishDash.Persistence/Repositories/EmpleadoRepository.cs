using DishDash.Domain.Entities;
using DishDash.Domain.Interfaces;
using DishDash.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DishDash.Persistence.Repositories;

public class EmpleadoRepository(DishDashDbContext context) : IEmpleadoRepository
{
    public async Task<Empleado?> ObtenerPorIdAsync(int id)
    {
        return await context.Empleados
            .Include(e => e.Puesto)
            .FirstOrDefaultAsync(e => e.EmpleadoId == id);
    }

    public async Task<IEnumerable<Empleado>> ObtenerTodosAsync()
    {
        return await context.Empleados
            .Include(e => e.Puesto)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Empleado> CrearAsync(Empleado empleado)
    {
        context.Empleados.Add(empleado);
        await context.SaveChangesAsync();
        return empleado;
    }

    public async Task<Empleado> ActualizarAsync(Empleado empleado)
    {
        context.Empleados.Update(empleado);
        await context.SaveChangesAsync();
        return empleado;
    }

    public async Task<bool> ExisteCedulaAsync(string cedula, int? excluirId = null)
    {
        var query = context.Empleados.Where(e => e.Cedula == cedula);
        if (excluirId.HasValue)
        {
            query = query.Where(e => e.EmpleadoId != excluirId.Value);
        }
        return await query.AnyAsync();
    }
}