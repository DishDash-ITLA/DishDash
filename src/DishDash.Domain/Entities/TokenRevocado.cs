using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Entities
{
    public class TokenRevocado
    {
        public int TokenId { get; set; }
        public int UsuarioId { get; set; }
        public string Jti { get; set; } = string.Empty;
        public DateTime ExpiraEn { get; set; }
        public DateTime RevocadoEn { get; set; } = DateTime.UtcNow;

        // Navigation
        public Usuario Usuario { get; set; } = null!;
    }
}
