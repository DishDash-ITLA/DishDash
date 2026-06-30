using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
        public DateTime? UltimoAcceso { get; set; }

        // Navigation
        public Rol Rol { get; set; } = null!;
        public ICollection<TokenRevocado> TokensRevocados { get; set; } = [];
    }
}
