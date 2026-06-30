using DishDash.Domain.Entities;
using DishDash.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using DishDash.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Persistence.Repositories
{
    public class ClienteRepository(DishDashDbContext ctx) : IClienteRepository
    {
        public async Task<Cliente?> ObtenerPorIdAsync(int id)
            => await ctx.Clientes.FindAsync(id);

        public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
            => await ctx.Clientes.OrderBy(c => c.Apellido).ThenBy(c => c.Nombre).ToListAsync();

        public async Task<Cliente> CrearAsync(Cliente cliente)
        {
            ctx.Clientes.Add(cliente);
            await ctx.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> ActualizarAsync(Cliente cliente)
        {
            ctx.Clientes.Update(cliente);
            await ctx.SaveChangesAsync();
            return cliente;
        }
    }
}
