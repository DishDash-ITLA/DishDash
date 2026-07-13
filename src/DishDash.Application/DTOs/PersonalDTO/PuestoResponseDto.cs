using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.PersonalDTO
{


    public record PuestoResponseDto(int PuestoId, string Nombre, string? Descripcion);

}
