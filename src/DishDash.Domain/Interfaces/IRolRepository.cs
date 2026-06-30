using DishDash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Interfaces
{
    public interface IRolRepository
    {
        Task<Rol?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Rol>> ObtenerTodosAsync();
    }
}
