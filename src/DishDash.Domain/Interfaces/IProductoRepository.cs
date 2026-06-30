using DishDash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Interfaces
{
    public interface IProductoRepository
    {
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<IEnumerable<Producto>> ObtenerConAlertasAsync();
        Task<Producto> CrearAsync(Producto producto);
        Task<Producto> ActualizarAsync(Producto producto);
        Task RegistrarMovimientoAsync(MovimientoInventario movimiento);
    }
}
