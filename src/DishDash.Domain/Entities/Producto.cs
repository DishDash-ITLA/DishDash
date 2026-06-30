using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
        public int UnidadId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal StockActual { get; set; } = 0;
        public decimal StockMinimo { get; set; } = 0;
        public decimal? StockMaximo { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
        public DateTime? ActualizadoEn { get; set; }

        // Navigation
        public Categoria Categoria { get; set; } = null!;
        public UnidadMedida Unidad { get; set; } = null!;
        public ICollection<MovimientoInventario> Movimientos { get; set; } = [];
        public ICollection<AlertaStock> Alertas { get; set; } = [];
    }
}
