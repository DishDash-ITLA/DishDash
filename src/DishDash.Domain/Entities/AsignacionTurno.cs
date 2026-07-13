using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class AsignacionTurno
    {
        public int AsignacionId { get; set; }
        public int EmpleadoId { get; set; }
        public int TurnoId { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Observacion { get; set; }
        public int? AsignadoPorId { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Empleado Empleado { get; set; } = null!;
        public Turno Turno { get; set; } = null!;
        public Usuario? AsignadoPor { get; set; }
        public ICollection<Asistencia> Asistencias { get; set; } = [];
    }
}
