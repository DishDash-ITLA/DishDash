using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class Asistencia
    {
        public int AsistenciaId { get; set; }
        public int EmpleadoId { get; set; }
        public int? AsignacionId { get; set; }
        public DateOnly Fecha { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        public string Estado { get; set; } = "Presente";
        public string? Observacion { get; set; }
        public int? RegistradoPorId { get; set; }

        public Empleado Empleado { get; set; } = null!;
        public AsignacionTurno? Asignacion { get; set; }
        public Usuario? RegistradoPor { get; set; }
    }
}
