using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class Mesa
    {
        public int MesaId { get; set; }
        public int Numero { get; set; }
        public int Capacidad { get; set; }
        public string? Ubicacion { get; set; }
        public string Estado { get; set; } = "Disponible";
        public bool Activa { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Reserva> Reservas { get; set; } = [];
    }
}
