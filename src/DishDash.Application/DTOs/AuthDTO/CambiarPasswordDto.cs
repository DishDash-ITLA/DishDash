using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.AuthDTO
{
    public record CambiarPasswordDto(
        [Required] string PasswordActual,
        [Required, MinLength(8)] string NuevoPassword,
        [Required] string ConfirmarPassword
    )

    {
        public bool PasswordsCoinciden => NuevoPassword == ConfirmarPassword;
    }
}
