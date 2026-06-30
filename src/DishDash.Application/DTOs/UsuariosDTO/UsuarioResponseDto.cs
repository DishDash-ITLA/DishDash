using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.UsuarioDTO
{
    public record UsuarioResponseDto(

        int UsuarioId,
        string Nombre,
        string Apellido,
        string Email,
        string Rol,
        int RolId,
        bool Activo,
        DateTime CreadoEn,
        DateTime? UltimoAcceso
    );
}
