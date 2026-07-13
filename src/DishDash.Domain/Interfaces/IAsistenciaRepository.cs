using DishDash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Interfaces
{
    public interface IAsistenciaRepository
    {
        Task<Asistencia> RegistrarAsync(Asistencia asistencia);
        Task<Asistencia?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Asistencia>> ObtenerPorFechaAsync(DateOnly fecha);
        Task<IEnumerable<Asistencia>> ObtenerPorEmpleadoAsync(int empleadoId);
        Task<Asistencia> ActualizarAsync(Asistencia asistencia);
    }
}
