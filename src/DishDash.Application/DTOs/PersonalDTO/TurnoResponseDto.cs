using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.PersonalDTO
{

    public record TurnoResponseDto(
    int TurnoId, string Nombre, TimeOnly HoraInicio, TimeOnly HoraFin
    );

}
