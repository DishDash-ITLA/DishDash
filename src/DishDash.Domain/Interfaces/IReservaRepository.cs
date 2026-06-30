using DishDash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Interfaces
{
    public interface IReservaRepository
    {
        Task<Reserva?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Reserva>> ObtenerTodasAsync();
        Task<IEnumerable<Reserva>> ObtenerPorFechaAsync(DateOnly fecha);
        Task<IEnumerable<Reserva>> ObtenerPorClienteAsync(int clienteId);
        Task<Reserva> CrearAsync(Reserva reserva);
        Task<Reserva> ActualizarAsync(Reserva reserva);
        Task<bool> HayConflictoMesaAsync(int mesaId, DateOnly fecha, TimeOnly hora, int? excluirId = null);
    }
}
