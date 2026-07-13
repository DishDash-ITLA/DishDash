using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class Evaluacion
    {
        public int EvaluacionId { get; set; }
        public int EmpleadoId { get; set; }
        public int EvaluadoPorId { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public decimal Puntuacion { get; set; }
        public string? Comentario { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public Empleado Empleado { get; set; } = null!;
        public Usuario EvaluadoPor { get; set; } = null!;
    }
}
