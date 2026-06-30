using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.UsuarioDTO
{
    public record ActualizarUsuarioDto(

        [Required, MaxLength(100)] string Nombre,
        [Required, MaxLength(100)] string Apellido,
        [Required, EmailAddress, MaxLength(150)] string Email,
        [Required] int RolId,
        bool Activo = true
    );
}
