using DishDash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();
        Task<Cliente> CrearAsync(Cliente cliente);
        Task<Cliente> ActualizarAsync(Cliente cliente);
    }
}
