using DishDash.Application.DTOs.UsuarioDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponseDto>> ObtenerTodosAsync();
        Task<UsuarioResponseDto> ObtenerPorIdAsync(int id);
        Task<UsuarioResponseDto> CrearAsync(CrearUsuarioDto dto);
        Task<UsuarioResponseDto> ActualizarAsync(int id, ActualizarUsuarioDto dto);
        Task EliminarAsync(int id);
    }
}
