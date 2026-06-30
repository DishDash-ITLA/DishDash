using DishDash.Domain.Entities;
using DishDash.Domain.Interfaces;
using DishDash.Persistence.Context;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Persistence.Repositories
{
    public class MesaRepository(DishDashDbContext ctx) : IMesaRepository
    {
        public async Task<Mesa?> ObtenerPorIdAsync(int id)
            => await ctx.Mesas.FindAsync(id);

        public async Task<IEnumerable<Mesa>> ObtenerTodasAsync()
            => await ctx.Mesas.Where(m => m.Activa).OrderBy(m => m.Numero).ToListAsync();

        public async Task<IEnumerable<Mesa>> ObtenerDisponiblesAsync(DateOnly fecha, TimeOnly hora, int personas)
        {
            // Mesas sin reserva activa en ese horario (ventana de 2 horas)
            var horaFin = hora.AddHours(2);
            var reservadas = await ctx.Reservas
                .Where(r => r.FechaReserva == fecha
                    && r.Estado != "Cancelada"
                    && r.HoraReserva < horaFin
                    && r.HoraReserva > hora.AddHours(-2))
                .Select(r => r.MesaId)
                .ToListAsync();

            return await ctx.Mesas
                .Where(m => m.Activa
                    && m.Estado == "Disponible"
                    && m.Capacidad >= personas
                    && !reservadas.Contains(m.MesaId))
                .OrderBy(m => m.Capacidad)
                .ToListAsync();
        }

        public async Task<Mesa> CrearAsync(Mesa mesa)
        {
            ctx.Mesas.Add(mesa);
            await ctx.SaveChangesAsync();
            return mesa;
        }

        public async Task<Mesa> ActualizarAsync(Mesa mesa)
        {
            ctx.Mesas.Update(mesa);
            await ctx.SaveChangesAsync();
            return mesa;
        }
    }
}
