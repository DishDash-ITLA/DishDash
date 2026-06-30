using DishDash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Interfaces
{
    public interface IMesaRepository
    {
        Task<Mesa?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Mesa>> ObtenerTodasAsync();
        Task<IEnumerable<Mesa>> ObtenerDisponiblesAsync(DateOnly fecha, TimeOnly hora, int personas);
        Task<Mesa> CrearAsync(Mesa mesa);
        Task<Mesa> ActualizarAsync(Mesa mesa);
    }
}
