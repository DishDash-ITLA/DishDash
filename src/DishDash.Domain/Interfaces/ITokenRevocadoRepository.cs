using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Interfaces
{
    public interface ITokenRevocadoRepository
    {
        Task RevocarAsync(string jti, int usuarioId, DateTime expiraEn);
        Task<bool> EstaRevocadoAsync(string jti);
        Task LimpiarExpiradosAsync();
    }
}
