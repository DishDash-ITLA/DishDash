using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.ReservasDTO
{
    public record ClienteResponseDto(

        int ClienteId,
        string Nombre,
        string Apellido,
        string? Email,
        string? Telefono,
        DateTime CreadoEn
    );
}
