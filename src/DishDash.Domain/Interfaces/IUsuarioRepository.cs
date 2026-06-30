using DishDash.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Domain.Interfaces
{
    public interface IUsuarioRepository
    {

        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<Usuario> CrearAsync(Usuario usuario);
        Task<Usuario> ActualizarAsync(Usuario usuario);
        Task EliminarAsync(int id);
        Task<bool> ExisteEmailAsync(string email, int? excluirId = null);

    }
}
