using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerarToken(Domain.Entities.Usuario usuario);
        string GenerarRefreshToken();
        (string jti, DateTime expira) ObtenerClaimsToken(string token);
    }
}
