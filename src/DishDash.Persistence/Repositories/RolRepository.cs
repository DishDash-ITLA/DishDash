using DishDash.Domain.Entities;
using DishDash.Domain.Interfaces;
using DishDash.Persistence.Context;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Persistence.Repositories
{
    public class RolRepository(DishDashDbContext ctx) : IRolRepository
    {
        public async Task<Rol?> ObtenerPorIdAsync(int id)
            => await ctx.Roles.FindAsync(id);

        public async Task<IEnumerable<Rol>> ObtenerTodosAsync()
            => await ctx.Roles.Where(r => r.Activo).OrderBy(r => r.Nombre).ToListAsync();
    }
}
