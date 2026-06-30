using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class AlertaStock
    {
        public int AlertaId { get; set; }
        public int ProductoId { get; set; }
        public string TipoAlerta { get; set; } = "StockBajo";
        public decimal StockAlMomento { get; set; }
        public bool Resuelta { get; set; } = false;
        public DateTime GeneradaEn { get; set; } = DateTime.UtcNow;
        public DateTime? ResueltaEn { get; set; }

        public Producto Producto { get; set; } = null!;
    }
}
