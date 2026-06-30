using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class MovimientoInventario
    {
        public int MovimientoId { get; set; }
        public int ProductoId { get; set; }
        public int TipoMovimientoId { get; set; }
        public int? UsuarioId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal StockAnterior { get; set; }
        public decimal StockResultante { get; set; }
        public string? Motivo { get; set; }
        public DateTime FechaMovimiento { get; set; } = DateTime.UtcNow;

        public Producto Producto { get; set; } = null!;
        public TipoMovimiento TipoMovimiento { get; set; } = null!;
        public Usuario? Usuario { get; set; }
    }
}
