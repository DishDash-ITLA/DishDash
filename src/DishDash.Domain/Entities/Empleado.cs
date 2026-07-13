using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class Empleado
    {
        public int EmpleadoId { get; set; }
        public int? UsuarioId { get; set; }
        public int PuestoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string? Cedula { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public DateOnly FechaIngreso { get; set; }
        public decimal? Salario { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
        public DateTime? ActualizadoEn { get; set; }

        public Usuario? Usuario { get; set; }
        public Puesto Puesto { get; set; } = null!;
        public ICollection<AsignacionTurno> Asignaciones { get; set; } = [];
        public ICollection<Asistencia> Asistencias { get; set; } = [];
        public ICollection<Evaluacion> Evaluaciones { get; set; } = [];
    }
}
