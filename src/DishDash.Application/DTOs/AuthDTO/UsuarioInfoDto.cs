using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.AuthDTO
{
    public record UsuarioInfoDto(

        int UsuarioId,
        string Nombre,
        string Apellido,
        string Email,
        string Rol
    );

}
