using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class HistorialReserva
    {
        public int HistorialId { get; set; }
        public int ReservaId { get; set; }
        public int? UsuarioId { get; set; }
        public string EstadoAnterior { get; set; } = string.Empty;
        public string EstadoNuevo { get; set; } = string.Empty;
        public string? Observacion { get; set; }
        public DateTime CambiadoEn { get; set; } = DateTime.UtcNow;

        // Navigation
        public Reserva Reserva { get; set; } = null!;
        public Usuario? Usuario { get; set; }
    }
}
