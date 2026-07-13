using DishDash.Domain.Entities;
using DishDash.Domain.Interfaces;
using DishDash.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DishDash.Persistence.Repositories;

public class PuestoRepository(DishDashDbContext context) : IPuestoRepository
{
    public async Task<Puesto?> ObtenerPorIdAsync(int id)
    {
        return await context.Puestos.FindAsync(id);
    }

    public async Task<IEnumerable<Puesto>> ObtenerTodosAsync()
    {
        return await context.Puestos.Where(p => p.Activo).AsNoTracking().ToListAsync();
    }
}