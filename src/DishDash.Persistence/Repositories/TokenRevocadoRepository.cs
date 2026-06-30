using DishDash.Domain.Entities;
using DishDash.Domain.Interfaces;
using DishDash.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Persistence.Repositories
{
    public class TokenRevocadoRepository(DishDashDbContext ctx) : ITokenRevocadoRepository
    {
        public async Task RevocarAsync(string jti, int usuarioId, DateTime expiraEn)
        {
            ctx.TokensRevocados.Add(new TokenRevocado
            {
                Jti = jti,
                UsuarioId = usuarioId,
                ExpiraEn = expiraEn
            });
            await ctx.SaveChangesAsync();
        }

        public async Task<bool> EstaRevocadoAsync(string jti)
            => await ctx.TokensRevocados.AnyAsync(t => t.Jti == jti);

        public async Task LimpiarExpiradosAsync()
        {
            var expirados = await ctx.TokensRevocados
                .Where(t => t.ExpiraEn < DateTime.UtcNow)
                .ToListAsync();
            ctx.TokensRevocados.RemoveRange(expirados);
            await ctx.SaveChangesAsync();
        }
    }
}
