using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Application.DTOs.PersonalDTO
{
    public record MarcarSalidaDto(
    [Required] DateTime HoraSalida
    );
}
