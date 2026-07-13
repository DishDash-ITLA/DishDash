using DishDash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<Empleado?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Empleado>> ObtenerTodosAsync();
        Task<Empleado> CrearAsync(Empleado empleado);
        Task<Empleado> ActualizarAsync(Empleado empleado);
        Task<bool> ExisteCedulaAsync(string cedula, int? excluirId = null);
    }
}
