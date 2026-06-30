using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class TipoMovimiento
    {
        public int TipoMovimientoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool EsEntrada { get; set; }

        public ICollection<MovimientoInventario> Movimientos { get; set; } = [];
    }
}
