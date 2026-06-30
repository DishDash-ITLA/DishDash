using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.AuthDTO
{
    public record LoginResponseDto(
        string Token,
        string RefreshToken,
        DateTime ExpiraEn,
        UsuarioInfoDto Usuario
    );

}
