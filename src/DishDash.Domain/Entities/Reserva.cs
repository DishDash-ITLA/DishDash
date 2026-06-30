using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public int ClienteId { get; set; }
        public int MesaId { get; set; }
        public int? UsuarioCreadoPorId { get; set; }
        public DateOnly FechaReserva { get; set; }
        public TimeOnly HoraReserva { get; set; }
        public int NumeroPersonas { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string? Notas { get; set; }
        public DateTime? NotificadoEn { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
        public DateTime? ActualizadoEn { get; set; }

        // Navigation
        public Cliente Cliente { get; set; } = null!;
        public Mesa Mesa { get; set; } = null!;
        public Usuario? UsuarioCreadoPor { get; set; }
        public ICollection<HistorialReserva> Historial { get; set; } = [];
    }
}
