using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class Rol
    {

        public int RolId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Usuario> Usuarios { get; set; } = [];
    }
}
