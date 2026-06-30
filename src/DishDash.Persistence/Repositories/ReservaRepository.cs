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
    public class ReservaRepository(DishDashDbContext ctx) : IReservaRepository
    {
        private IQueryable<Reserva> Query() =>
            ctx.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Mesa)
                .Include(r => r.UsuarioCreadoPor);

        public async Task<Reserva?> ObtenerPorIdAsync(int id)
            => await Query().Include(r => r.Historial)
                .FirstOrDefaultAsync(r => r.ReservaId == id);

        public async Task<IEnumerable<Reserva>> ObtenerTodasAsync()
            => await Query().OrderByDescending(r => r.FechaReserva)
                .ThenBy(r => r.HoraReserva).ToListAsync();

        public async Task<IEnumerable<Reserva>> ObtenerPorFechaAsync(DateOnly fecha)
            => await Query().Where(r => r.FechaReserva == fecha)
                .OrderBy(r => r.HoraReserva).ToListAsync();

        public async Task<IEnumerable<Reserva>> ObtenerPorClienteAsync(int clienteId)
            => await Query().Where(r => r.ClienteId == clienteId)
                .OrderByDescending(r => r.FechaReserva).ToListAsync();

        public async Task<Reserva> CrearAsync(Reserva reserva)
        {
            ctx.Reservas.Add(reserva);
            await ctx.SaveChangesAsync();
            // Reload navigation properties
            return await ObtenerPorIdAsync(reserva.ReservaId) ?? reserva;
        }

        public async Task<Reserva> ActualizarAsync(Reserva reserva)
        {
            ctx.Reservas.Update(reserva);
            await ctx.SaveChangesAsync();
            return await ObtenerPorIdAsync(reserva.ReservaId) ?? reserva;
        }

        public async Task<bool> HayConflictoMesaAsync(int mesaId, DateOnly fecha, TimeOnly hora, int? excluirId = null)
        {
            var ventana = hora.AddHours(-2);
            return await ctx.Reservas.AnyAsync(r =>
                r.MesaId == mesaId
                && r.FechaReserva == fecha
                && r.Estado != "Cancelada"
                && r.HoraReserva > ventana
                && r.HoraReserva < hora.AddHours(2)
                && (excluirId == null || r.ReservaId != excluirId));
        }
    }
}
