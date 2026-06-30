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
    public class UsuarioRepository(DishDashDbContext ctx) : IUsuarioRepository
    {
        public async Task<Usuario?> ObtenerPorIdAsync(int id)
            => await ctx.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.UsuarioId == id);

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
            => await ctx.Usuarios.Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == email.ToLower().Trim());

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
            => await ctx.Usuarios.Include(u => u.Rol).OrderBy(u => u.Nombre).ToListAsync();

        public async Task<Usuario> CrearAsync(Usuario usuario)
        {
            usuario.Email = usuario.Email.ToLower().Trim();
            ctx.Usuarios.Add(usuario);
            await ctx.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> ActualizarAsync(Usuario usuario)
        {
            ctx.Usuarios.Update(usuario);
            await ctx.SaveChangesAsync();
            return usuario;
        }

        public async Task EliminarAsync(int id)
        {
            var u = await ctx.Usuarios.FindAsync(id);
            if (u is not null)
            {
                u.Activo = false;
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteEmailAsync(string email, int? excluirId = null)
            => await ctx.Usuarios.AnyAsync(u =>
                u.Email == email.ToLower().Trim() &&
                (excluirId == null || u.UsuarioId != excluirId));
    }
}
