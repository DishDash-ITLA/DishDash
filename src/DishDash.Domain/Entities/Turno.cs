using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class Turno
    {
        public int TurnoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public bool Activo { get; set; } = true;

        public ICollection<AsignacionTurno> Asignaciones { get; set; } = [];
    }
}
