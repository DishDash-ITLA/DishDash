using DishDash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Interfaces
{
    public interface ITurnoRepository
    {
        Task<Turno?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Turno>> ObtenerTodosAsync();
        Task<AsignacionTurno> AsignarAsync(AsignacionTurno asignacion);
        Task<IEnumerable<AsignacionTurno>> ObtenerAsignacionesPorFechaAsync(DateOnly fecha);
        Task<IEnumerable<AsignacionTurno>> ObtenerAsignacionesPorEmpleadoAsync(int empleadoId);
    }
}
